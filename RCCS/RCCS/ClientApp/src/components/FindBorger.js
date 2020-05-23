import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {Button} from "reactstrap";
import '../CSS/StyleSheet.css'

export class FindBorger extends Component {
    static displayName = FindBorger.name;

    constructor(props) {
        super(props);
        this.state = {findborgers: [], loading: true, search: ''};
    }

    componentDidMount() {
        this.populateBorgerData();
    }

    updateSearch(event) {
        this.setState({search: event.target.value.substr(0, 20)});
    }

    static renderFindBorgerTable(findborgers) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Cpr-nr</th>
                    <th>Navn</th>
                    <th>Aflastningsplads</th>
                    <th>Tid til udskrivning</th>
                    <th>Status</th>
                    <th>Vis Borger</th>
                </tr>
                </thead>
                <tbody>
                {findborgers.map(borger =>
                    <tr key={borger.cpr}>
                        <td>{borger.cpr}</td>
                        <td>{borger.citizenName}</td>
                        <td>{borger.respiteCareHome}</td>
                        <td>{borger.timeUntilDischarge}</td>
                        <td>{borger.prospectiveSituationStatusForCitizen}</td>
                        <td>
                            {<Link to={{pathname: "./BorgerVisning/" + borger.cpr}} className="btn btn-primary"
                                   color="white">Vis {borger.citizenName}</Link>}
                        </td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }

    render() {
        let filteredborgers = this.state.findborgers.filter(
            (FindBorger) => {
                return FindBorger.citizenName.toLowerCase().indexOf(this.state.search.toLowerCase()) !== -1;
            }
        );

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FindBorger.renderFindBorgerTable(filteredborgers);
        //this.state.findborgers

        return (

            <div>
                <head>link rel = "searchbar" href = "StyleSheet.css"</head>

                <h1 id="tabelLabel">Borger liste</h1>
                <p>Her kan der ses en liste over borgere i systemet.</p>

                
                    <input  type="text"
                    value={this.state.search}
                    onChange={this.updateSearch.bind(this)}
                        placeholder="Søg..." />
                  
             
                
              
                    {contents}
                <Link to={{ pathname: "/" }} className="btn btn-primary">Tilbage</Link>
                </div>
            
        );
    }

    async populateBorgerData() {
        const that = this;
        const response = await fetch("https://localhost:44356/rccsdb/citizenlist", {
            method: 'GET',  // Or POST, PUT, DELETE
            credentials: 'include',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("jwt"),
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            if (response.ok) {
                response.json().then(function (data) {
                    if (data != null) {
                        that.setState({findborgers: data, loading: false});
                    }
                })
            }
        }).catch(error => {
            console.error('Caught error:', error);
            that.setState({
                Error: true
            });
        });
    }
}

import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {Button} from 'reactstrap';

export class BoligListe extends React.Component {
    static displayName = BoligListe.name;

    constructor(props) {
        super(props);
        this.state = {boligliste: [], loading: true, available: null};
    }

    componentDidMount() {
        this.populateBoligData();
    }

    static renderBoligTable(boligliste, available) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Navn</th>
                    <th>Type</th>
                    <th>Adresse</th>
                    <th>Ledige pladser</th>
                    <th>Maks antal pladser</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {boligliste.map(bolig =>
                    <tr key={bolig.respiteCareHome + bolig.type}>
                        <td>{bolig.respiteCareHome}</td>
                        <td>{bolig.type}</td>
                        <td>{bolig.address}</td>
                        <td>{bolig.availableRespiteCareRooms}</td>
                        <td>{bolig.respiteCareRoomsTotal}</td>
                        <td>

                            <Link
                                to={{pathname: "./opretborger", state: {type: bolig.type, name: bolig.respiteCareHome}}}
                                className="btn btn-primary" color="white">Opret Borger</Link>

                        </td>
                    </tr>
                    )}
                   
                </tbody>
                
            </table>
        );
    }


    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : BoligListe.renderBoligTable(this.state.boligliste, this.state.available);


        return (
            <div>
                <h1 id="tabelLabel">Bolig liste</h1>
                <p>Her kan der ses en liste over boliger i systemet.</p>
                {contents}
                <Link to={{ pathname: "/" }} className="btn btn-primary">Tilbage</Link>
            </div>
        );
    }

    async populateBoligData() {
        const that = this;
        const response = await fetch("https://localhost:44356/rccsdb/RespiteCareHomeList", {
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
                        that.setState({boligliste: data, loading: false});
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



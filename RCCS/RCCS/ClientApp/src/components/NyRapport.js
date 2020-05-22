import React, { Component } from 'react';
import {Link} from "react-router-dom";

export class NyRapport extends Component {
    static displayName = NyRapport.name;

    constructor(props) {
        let url = window.location.pathname.split("/");

        super(props);
        this.state = { borger: [], loading: true, cpr: url[2], Report: "", ResponsibleCaretaker: "", Title: "" };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleTitle = this.handleTitle.bind(this);
        this.handleReport = this.handleReport.bind(this);
        this.handleResponsibleCaretaker = this.handleResponsibleCaretaker.bind(this);
    }

    componentDidMount() {
        this.populateBorgerData();
    }

    handleSubmit(event) {
        var url1 = "https://localhost:44356/rccsdb/CreateProgressReport"
        event.preventDefault();
        fetch(url1, {
            method: 'POST',
            body: JSON.stringify({
                "cpr": Number(this.state.borger.cpr),
                "title": this.state.Title,
                "report": this.state.Report,
                "responsibleCaretaker": this.state.ResponsibleCaretaker
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(responseJson => {
            JSON.parse(responseJson);
        })
            .catch(error => { alert("Fejl" + error); });
    }

    handleReport = (event) => {
        this.setState({ Report: event.target.value });
    }

    handleResponsibleCaretaker(event) {
        this.setState({ ResponsibleCaretaker: event.target.value });
    }

    handleTitle(event) {
        this.setState({ Title: event.target.value });
    }

    render() {

        return (
            <div>
                <h1>Skriv ny rapport</h1>
                <table>
                    <tr>
                        <th>
                            <b>Borger navn: </b> {this.state.borger.name}, <b>CPR: </b> {this.state.borger.cpr}
                        </th>
                        <th></th>
                        <th>
                            <Link to={{ pathname: "/BorgerVisning/" + this.state.borger.cpr }} className="btn btn-primary" onClick={this.handleSubmit} color="white">Gem</Link>
                            <button onClick={this.handleSubmit}>Gem rapport</button>
                        </th>
                    </tr>
                    <tr>
                        <table>
                            <tr>
                                <td>
                                    <b>Aflastningssted</b>
                                </td>
                                <td>
                                    {this.state.borger.respiteCareHomeName}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Udskrivningsdato</b>
                                </td>
                                <td>
                                    {this.state.borger.plannedDischargeDate}
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>ResponsibleCaretaker</b>
                                </td>
                                <td>
                                    <input type="text" onChange={this.handleResponsibleCaretaker} />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Title på evaluation</b>
                                </td>
                                <td>
                                    <input type="text" onChange={this.handleTitle} />
                                </td>
                            </tr>
                        </table>
                        <td>
                            Status rapport: <br />
                            <textarea placeholder="Skriv statusrapport" rows="10" cols="60" onChange={this.handleReport} />
                        </td>
                    </tr>
                    <tr>
                        <Link to={{ pathname: "/BorgerVisning/" + this.state.borger.cpr }} className="btn btn-primary">tilbage</Link>
                    </tr>
                </table>

            </div>
        );
    }


    async populateBorgerData() {
        const response = await fetch("https://localhost:44356/rccsdb/CreateProgressReport/"+this.state.cpr );
        const data = await response.json();
        this.setState({ borger: data, loading: false });
    }

}

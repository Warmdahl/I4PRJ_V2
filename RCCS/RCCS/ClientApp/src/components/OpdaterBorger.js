import React, { Component } from 'react';
import { Link, Route } from "react-router-dom";
import { Label } from "reactstrap";
//import axios from 'axios';


export class OpdaterBorger extends Component {
    static displayName = OpdaterBorger.name;

    constructor(props) {
        super(props);
        let url = window.location.pathname.split("/");
        this.state = {
            borger: [], cpr: url[2], FirstName: "", lastName: "Test", relativeFirstName: "",
            relativeLastName: "", relativePhonenumber: 1, relativeRelation: "", relativeIsPrimary: true,
            startDate: null, reevaluationDate: null, plannedDischarge: null, prospectiveSituation: "test",
            careNeed: "", purposeOfStay: "", currentStatus: "", numberOfReevlaluations: 0
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChangeCpr = this.handleChangeCpr.bind(this);
        this.handleChangeFirstname = this.handleChangeFirstname.bind(this);
        this.handleChangeLastname = this.handleChangeLastname.bind(this);
        this.handleChangeStartdato = this.handleChangeStartdato.bind(this);
        this.handleChangeCareNeed = this.handleChangeCareNeed.bind(this);
        this.handleChangeCurrentStatus = this.handleChangeCurrentStatus.bind(this);
        this.handleChangeNumberOfReevaluations = this.handleChangeNumberOfReevaluations.bind(this);
        this.handleChangePlannedDischarge = this.handleChangePlannedDischarge.bind(this);
        this.handleChangePurposeOfStay = this.handleChangePurposeOfStay.bind(this);
        this.handleChangeReevalutationDate = this.handleChangeReevalutationDate.bind(this);
        this.handleChangeRelativeFirstName = this.handleChangeRelativeFirstName.bind(this);
        this.handleChangeRelativeIsPrimary = this.handleChangeRelativeIsPrimary.bind(this);
        this.handleChangeRelativeLastName = this.handleChangeRelativeLastName.bind(this);
        this.handleChangeRelativePhonenumber = this.handleChangeRelativePhonenumber.bind(this);
        this.handleChangeRelativeRelation = this.handleChangeRelativeRelation.bind(this);
        this.handleChangeProspectiveSituation = this.handleChangeProspectiveSituation.bind(this);

    }

    componentDidMount() {
        this.populateBorgerData();
    }

    handleSubmit(event) {
        var url = "https://localhost:44356/api/Citizen"
        event.preventDefault();
        fetch(url, {
            method: 'POST',
            //credentials: 'include',
            body: JSON.stringify({
                "firstName": this.state.FirstName,
                "lastName": this.state.lastName,
                "cpr": Number(this.state.cpr),
                "relativeFirstName": this.state.relativeFirstName,
                "relativeLastName": this.state.relativeLastName,
                "phonenumber": Number(this.state.relativePhonenumber),
                "relation": this.state.relativeRelation,
                "isPrimary": this.state.relativeIsPrimary,
                "startDate": this.state.startDate,
                "reevaluationDate": this.state.reevaluationDate,
                "plannedDischargeDate": this.state.plannedDischarge,
                "prospectiveSituationStatusForCitizen": this.state.prospectiveSituation,
                "careNeed": this.state.careNeed,
                "purposeOfStay": this.state.purposeOfStay,
                "currentStatus": this.state.currentStatus,
                "numberOfReevaluations": Number(this.state.numberOfReevlaluations)
            }),
            headers: {
                //'Authorization': 'Bearer' + localStorage.getItem("token"),
                'Content-Type': 'application/json'
            }
        }).then(responseJson => {
            JSON.parse(responseJson);
        })
            .catch(error => { alert("fejl" + error); });
    }

    /*se(e) {
        e.preventDefault();
        alert(this.state.relativeIsPrimary)
    }*/

    handleChangeFirstname = (event) => {
        this.setState({ FirstName: event.target.value });

    }

    handleChangeLastname(event) {
        this.setState({ lastName: event.target.value });

    }

    handleChangeCpr(event) {
        this.setState({ cpr: event.target.value });
    }

    handleChangeRelativeFirstName(event) {
        this.setState({ relativeFirstName: event.target.value });
    }

    handleChangeRelativeLastName(event) {
        this.setState({ relativeLastName: event.target.value });
    }

    handleChangeCareNeed(event) {
        this.setState({ handleChangeCareNeed: event.target.value });
    }

    handleChangeCurrentStatus(event) {
        this.setState({ currentStatus: event.target.value });
    }

    handleChangeNumberOfReevaluations(event) {
        this.setState({ numberOfReevlaluations: event.target.value });
    }

    handleChangePlannedDischarge(event) {
        this.setState({ plannedDischarge: event.target.value });
    }

    handleChangePurposeOfStay(event) {
        this.setState({ purposeOfStay: event.target.value });
    }

    handleChangeReevalutationDate(event) {
        this.setState({ reevaluationDate: event.target.value });
    }

    handleChangeRelativeIsPrimary(event) {
        var b
        if (event.target.value === "v1") {
            b = true
        } else {
            b = false
        }

        this.setState({ relativeIsPrimary: b });

    }

    handleChangeRelativePhonenumber(event) {
        this.setState({ relativePhonenumber: event.target.value });
    }

    handleChangeRelativeRelation(event) {
        this.setState({ relativeRelation: event.target.value });
    }

    handleChangeStartdato(event) {
        this.setState({ startDate: event.target.value });
    }

    handleChangeProspectiveSituation(event) {
        this.setState({ prospectiveSituation: event.target.value });
    }

    handleChangeCareNeed(event) {
        this.setState({ careNeed: event.target.value })
    }






    render() {

        return (
            <div>
                <h1>Borger oplysninger</h1>
                <div>
                    <form >
                        <label>Borger:{this.state.borger.cpr}</label><br />
                        <label>Fornavn: {this.state.borger.firstName}</label><br />
                        <input type="text" onChange={this.handleChangeFirstname} ></input><br />
                        <label>Efternavn {this.state.borger.lastName}</label><br />
                        <input type="text" onChange={this.handleChangeLastname} ></input><br />
                        <label>Cpr-nummer</label><br />
                        <input type="number" onChange={this.handleChangeCpr} ></input><br />
                        <label></label><br />
                        <label>Pårørende:</label><br />
                        <label>Pårørendes fornavn</label><br />
                        <input type="text" onChange={this.handleChangeRelativeFirstName} ></input><br />
                        <label>Pårørendes efternavn</label><br />
                        <input type="text" onChange={this.handleChangeRelativeLastName}></input><br />
                        <label>Pårørendes telefonnummer</label><br />
                        <input type="number" onChange={this.handleChangeRelativePhonenumber}></input><br />
                        <label>Relation</label><br />
                        <input type="text" onChange={this.handleChangeRelativeRelation} ></input><br />
                        <label>Primær pårørende</label><br />
                        <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === true} value="v1"></input>Ja</label><br />
                        <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === false} value="v2"></input>Nej</label><br />
                        <label></label><br />
                        <label>Opholdsinformation:</label><br />
                        <label>Startdato</label><br />
                        <input type="date" onChange={this.handleChangeStartdato} ></input><br />
                        <label>Reevalueringsdato</label><br />
                        <input type="date" onChange={this.handleChangeReevalutationDate} ></input><br />
                        <label>Planlagt udskrivning</label><br />
                        <input type="date" onChange={this.handleChangePlannedDischarge} ></input><br />
                        <label>Borgers fremtidige situation</label><br />
                        <select onChange={this.handleChangeProspectiveSituation}>
                            <option value="I bedring">I bedring</option>
                            <option value="Uændret">Uændret</option>
                            <option value="I forværring">I forværring</option>
                        </select><br />
                        <label>Borgeroverblik og statushistorik:</label><br />
                        <label>Plejebehov</label><br />
                        <select onChange={this.handleChangeCareNeed}>
                            <option value="Lille">Lille</option>
                            <option value="Mellem">Mellem</option>
                            <option value="Stor">Stor</option>
                        </select><br />
                        <label>Mål for ophold</label><br />
                        <input type="text" onChange={this.handleChangePurposeOfStay} ></input><br />
                        <label>Nuværende status</label><br />
                        <input type="text" onChange={this.handleChangeCurrentStatus} ></input><br />
                        <label>Antal reevalueringer</label><br />
                        <input type="number" onChange={this.handleChangeNumberOfReevaluations} ></input><br />
                        <button onClick={this.handleSubmit}>Gem</button>
                    </form>
                </div>
            </div>
        );
    }


    async populateBorgerData() {

        /*let jwt = localStorage.getItem("token")
        console.log(jwt)
        let jwtData = jwt.split('.')[1]
        console.log(jwtData)
        let decoded = window.atob(jwtData)
        console.log(decoded)
        let decodedData = JSON.parse(decoded)
        this.id = decodedData['Role']*/

        const response = await fetch('https://localhost:44356/rccsdb/citizen/' + this.state.cpr);
        const data = await response.json();
        this.setState({ borger: data, loading: false });
    }

}

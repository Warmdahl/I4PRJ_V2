import React, { Component } from 'react';
import { Link, Route } from "react-router-dom";
import { Label } from "reactstrap";



export class OpdaterBorger extends Component {
    static displayName = OpdaterBorger.name;

    constructor(props) {
        super(props);
        let url = window.location.pathname.split("/");
        this.state = {
            borger: [], cpr: url[2], FirstName: "", lastName: "Test", relativeFirstName: "",
            relativeLastName: "", relativePhonenumber: 1, relativeRelation: "", relativeIsPrimary: true,
            startDate: null, reevaluationDate: null, plannedDischarge: null, prospectiveSituation: "test",
            careNeed: "", purposeOfStay: ""
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        //this.handleChangeCpr = this.handleChangeCpr.bind(this);
        this.handleChangeFirstname = this.handleChangeFirstname.bind(this);
        this.handleChangeLastname = this.handleChangeLastname.bind(this);
        this.handleChangeStartdato = this.handleChangeStartdato.bind(this);
        this.handleChangeCareNeed = this.handleChangeCareNeed.bind(this);
        //this.handleChangeCurrentStatus = this.handleChangeCurrentStatus.bind(this);
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
        var url = "https://localhost:44356/rccsdb/createcitizen"
        event.preventDefault();
        fetch(url, {
            method: 'PUT',
            //credentials: 'include',
            body: JSON.stringify({
                "firstName": this.state.FirstName,
                "lastName": this.state.lastName,
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

    handleChangeFirstname = (event) => {
        this.setState({ FirstName: event.target.value });

    }

    handleChangeLastname(event) {
        this.setState({ lastName: event.target.value });

    }

    //handleChangeCpr(event) {
    //    this.setState({ cpr: event.target.value });
    //}

    handleChangeRelativeFirstName(event) {
        this.setState({ relativeFirstName: event.target.value });
    }

    handleChangeRelativeLastName(event) {
        this.setState({ relativeLastName: event.target.value });
    }

    handleChangeCareNeed(event) {
        this.setState({ handleChangeCareNeed: event.target.value });
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
                <h3>Ændr oplysninger for borger {this.state.borger.cpr}</h3>
                <div>
                    <form >
                        <label>Borger:</label><br />
                        <label>Fornavn nuværende: {this.state.borger.firstName}</label><br />
                        <input type="text" onChange={this.handleChangeFirstname}  ></input><br />
                        <label>Efternavn nuværende: {this.state.borger.lastName}</label><br />
                        <input type="text" onChange={this.handleChangeLastname}  ></input><br />
                        <label></label><br />

                        <label>Pårørende:</label><br />
                        <label>Pårørendes fornavn nuværende: {this.state.relativeFirstName}</label><br />
                        <input type="text" onChange={this.handleChangeRelativeFirstName} ></input><br />
                        <label>Pårørendes efternavn nuværende: {this.state.relativeLastName}</label><br />
                        <input type="text" onChange={this.handleChangeRelativeLastName} ></input><br />
                        <label>Pårørendes telefonnummer nuværende: {this.state.relativePhonenumber}</label><br />
                        <input type="number" onChange={this.handleChangeRelativePhonenumber} ></input><br />
                        <label>Relation nuværende: {this.state.relativeRelation}</label><br />
                        <input type="text" onChange={this.handleChangeRelativeRelation} ></input><br />
                        <label>Primær pårørende nuværende: {this.state.relativeIsPrimary}</label><br />
                        <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === true} value="v1"></input>Ja</label><br />
                        <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === false} value="v2"></input>Nej</label><br />
                        <label></label><br />

                        <label>Opholdsinformation:</label><br />
                        <label>Startdato nuværende: {this.state.startDate}</label><br />
                        <input type="date" onChange={this.handleChangeStartdato} ></input><br />
                        <label>Reevalueringsdato nuværende: {this.state.reevaluationDate}</label><br />
                        <input type="date" onChange={this.handleChangeReevalutationDate} ></input><br />
                        <label>Planlagt udskrivning nuværende: {this.state.plannedDischarge}</label><br />
                        <input type="date" onChange={this.handleChangePlannedDischarge} ></input><br />
                        <label>Borgers fremtidige situation nuværende: {this.state.prospectiveSituation}</label><br />
                        <select onChange={this.handleChangeProspectiveSituation}>
                            <option value="I bedring">I bedring</option>
                            <option value="Uændret">Uændret</option>
                            <option value="I forværring">I forværring</option>
                        </select><br />
                        <label>Borgeroverblik og statushistorik:</label><br />
                        <label>Plejebehov nuværende: {this.state.careNeed}</label><br />
                        <select onChange={this.handleChangeCareNeed}>
                            <option value="Lille">Lille</option>
                            <option value="Mellem">Mellem</option>
                            <option value="Stor">Stor</option>
                        </select><br />
                        <label>Mål for ophold nuværende: {this.state.purposeOfStay}</label><br />
                        <input type="text" onChange={this.handleChangePurposeOfStay} ></input><br />
                       
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

import React, { Component } from 'react';
import { Link } from "react-router-dom";
import '../CSS/StyleSheet.css';
import {Button} from 'reactstrap'

export class OpretBorger extends React.Component {
    static displayName = OpretBorger.name;

    constructor(props) {
        super(props);
        this.state = {
            type: props.type, home: null, FirstName: "", lastName: "Test", cpr: 1, relativeFirstName: "",
            relativeLastName: "", relativePhonenumber: 1, relativeRelation: "", relativeIsPrimary: true,
            startDate: null, reevaluationDate: null, plannedDischarge: null, prospectiveSituation: "test",
            careNeed: "", purposeOfStay: "", currentStatus: "", numberOfReevlaluations: 0
        };
        //this.se = this.se.bind(this);
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
        let temp = 0
        if (this.props.location.state.type === "Demensbolig") {
            temp = 1
        }
        this.setState({ type: temp, home: this.props.location.state.name })
        console.log(temp)
    }

    

                  

    handleSubmit(event) {
        var url = "https://localhost:44356/rccsdb/createcitizen"
        event.preventDefault();
        fetch(url, {
            method: 'POST',
            credentials: 'include',
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
                "respiteCareHomeName": this.state.home,
                "type": Number(this.state.type)
            }),
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("jwt"),
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
            b=false
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
            <div style={{
                position: 'absolute', left: '50%',
                transform: 'translate(-50%)'
            }}>
                <h1>Opret borger</h1>
                
                    <form >
                        <table border-spacing="10" margin="10">
                            <th>
                                <label>Borger:</label><br />
                            </th>
                            <tr>
                                <td>
                                    <label>Fornavn</label><br />
                                    <input type="text" onChange={this.handleChangeFirstname} ></input><br />
                                </td>
                                <td>
                                    <label>Efternavn</label><br />
                                    <input type="text" onChange={this.handleChangeLastname} ></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Cpr-nummer</label><br />
                                    <input type="number" onChange={this.handleChangeCpr} min="100000000" ></input><br />
                                </td>
                                <td>

                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>Pårørende:</label><br />
                                    
                                </td>
                                <td>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Pårørendes fornavn</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeFirstName} ></input><br />
                                </td>
                                <td>
                                    <label>Pårørendes efternavn</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeLastName}></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Pårørendes telefonnummer</label><br />
                                    <input type="number" onChange={this.handleChangeRelativePhonenumber} min="10000000" ></input><br />
                                </td>
                                <td>
                                    <label>Relation</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeRelation} ></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Primær pårørende</label><br />
                                    <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === true} value="v1"></input>Ja</label><br />
                                    <label><input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === false} value="v2"></input>Nej</label><br />
                                    <label></label><br />
                                </td>
                                <td>

                                </td>
                            </tr>
                            <label>Opholdsinformation:</label><br />
                            <tr>
                                <td>
                                    <label>Startdato</label><br />
                                    <input type="date" onChange={this.handleChangeStartdato} ></input><br />
                                </td>
                                <td>
                                    <label>Reevalueringsdato</label><br />
                                    <input type="date" onChange={this.handleChangeReevalutationDate} ></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Planlagt udskrivning</label><br />
                                    <input type="date" onChange={this.handleChangePlannedDischarge} ></input><br />
                                </td>
                                <td>
                                    <label>Borgers fremtidige situation</label><br />
                                    <select onChange={this.handleChangeProspectiveSituation}>
                                        <option value="I bedring">I bedring</option>
                                        <option value="Uændret">Uændret</option>
                                        <option value="I forværring">I forværring</option>
                                    </select><br />
                                </td>
                            </tr>
                            <label>Borgeroverblik og statushistorik:</label><br />
                            <tr>
                                <td>
                                    <label>Mål for ophold</label><br />
                                    <input type="text" onChange={this.handleChangePurposeOfStay} ></input><br />
                                </td>
                                <td>
                                    <label>Plejebehov</label><br />
                                    <select onChange={this.handleChangeCareNeed}>
                                        <option value="Lille">Lille</option>
                                        <option value="Mellem">Mellem</option>
                                        <option value="Stor">Stor</option>
                                    </select><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <Link to={{ pathname: "/boligliste" }} className="btn btn-primary">tilbage</Link>
                                </td>
                                <td>
                                    <Button color="primary" onClick={this.handleSubmit}>Gem</Button>
                                </td>
                            </tr>
                        </table>
                    </form>
                
                
            </div>
        );
    }

}

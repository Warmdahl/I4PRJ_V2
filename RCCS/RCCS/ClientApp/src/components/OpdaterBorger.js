import React, { Component } from 'react';
import { Link } from "react-router-dom";
import '../CSS/StyleSheet.css';
import { Button } from 'reactstrap';



export class OpdaterBorger extends React.Component {
    static displayName = OpdaterBorger.name;

    constructor(props) {
        super(props);
        let url = window.location.pathname.split("/");
        this.state = {
            borger: [], cpr: url[2], type: props.type, home: props.home, FirstName: "", lastName: "Test", relativeFirstName: "",
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
        //this.populateBorgerData();
        let temp = 0
        if (this.props.location.state.type === "Demensbolig") {
            temp = 1
        }
        this.setState({ type: temp, home: this.props.location.state.name })
    }

    handleSubmit(event) {
        var url = "https://localhost:44356/rccsdb/createcitizen"
        event.preventDefault();
        fetch(url, {
            method: 'PUT',
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
                'Authorization': 'Bearer' + localStorage.getItem("token"),
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

    //{this.state.borger.cpr}
    render() {

        return (
            <div style={{
                position: 'absolute', left: '50%',
                transform: 'translate(-50%)'
            }}>
                <h1>Borger oplysninger</h1>
                <h3>�ndr oplysninger for borger </h3>
                <div>
                    <form>
                        <table>
                            <th>
                                <label>Borger:{this.state.borger.cpr}</label><br />
                            </th>
                            <tr>
                                <td>
                                    <label>Fornavn: {this.state.borger.firstName}</label><br />
                                    <input type="text" onChange={this.handleChangeFirstname} ></input><br />
                                </td>
                                <td>
                                    <label>Efternavn {this.state.borger.lastName}</label><br />
                                    <input type="text" onChange={this.handleChangeLastname} ></input><br />
                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>P�r�rende:</label><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>P�r�rendes fornavn</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeFirstName} ></input><br />
                                </td>
                                <td>
                                    <label>P�r�rendes efternavn</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeLastName}></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>P�r�rendes telefonnummer</label><br />
                                    <input type="number" onChange={this.handleChangeRelativePhonenumber}></input><br />
                                </td>
                                <td>
                                    <label>Relation</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeRelation} ></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Prim�r p�r�rende</label><br />
                                    <input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === true} value="v1"></input><br />
                                    <input type="radio" onChange={this.handleChangeRelativeIsPrimary} checked={this.state.relativeIsPrimary === false} value="v2"></input><br />
                                </td>
                                <td>
                                    <br />
                                    <label>Ja</label><br/>
                                    <label>Nej</label><br/>
                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>Opholdsinformation:</label><br />
                                </td>
                            </tr>
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
                                        <option value="U�ndret">U�ndret</option>
                                        <option value="I forv�rring">I forv�rring</option>
                                    </select><br />
                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>Borgeroverblik og statushistorik:</label><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>M�l for ophold</label><br />
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
                                    <label>Nuv�rende status</label><br />
                                    <input type="text" onChange={this.handleChangeCurrentStatus} ></input><br />
                                </td>
                                <td>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <Button color="primary" onClick={this.handleSubmit}>Gem</Button>
                                </td>
                                <td>
                                    <Link to={{ pathname: "/" }} className="btn btn-primary">tilbage</Link>
                                </td>
                            </tr>
                        </table>
                    </form>
                </div>
            </div>
        );
    }


    //async populateBorgerData() {

    //////    /*let jwt = localStorage.getItem("token")
    //////    console.log(jwt)
    //////    let jwtData = jwt.split('.')[1]
    //////    console.log(jwtData)
    //////    let decoded = window.atob(jwtData)
    //////    console.log(decoded)
    //////    let decodedData = JSON.parse(decoded)
    //////    this.id = decodedData['Role']*/



    //    //const response = await fetch('https://localhost:44356/rccsdb/CitizenInformation/' + this.state.cpr);


    //    const response = await fetch('https://localhost:44356/rccsdb/CitizenInformation/' + this.state.cpr, {
    //        method: 'GET',
    //        credentials: 'include',
    //        headers: {
    //            'Authorization': 'Bearer' + localStorage.getItem("token"),
    //            'Content-Type': 'application/json'
    //        }
    //    })
    //    const data = await response.json();
    //    this.setState({ borger: data, loading: false });
    //}
}
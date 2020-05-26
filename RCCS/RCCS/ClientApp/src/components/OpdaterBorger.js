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
            borger: [], cpr: url[2], type: props.type, home: props.home, firstName: "", lastName: "", relativeFirstName: "",
            relativeLastName: "", relativePhonenumber: 1, relativeRelation: "", relativeIsPrimary: true,
            startDate: null, reevaluationDate: null, plannedDischarge: null, prospectiveSituation: "",
            careNeed: "", purposeOfStay: ""
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChangeFirstname = this.handleChangeFirstname.bind(this);
        this.handleChangeLastname = this.handleChangeLastname.bind(this);
        this.handleChangeStartdato = this.handleChangeStartdato.bind(this);
        this.handleChangeCareNeed = this.handleChangeCareNeed.bind(this);
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
            credentials: 'include',
            body: JSON.stringify({
                "firstName": this.state.firstName,
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

    handleChangeFirstname(event) {
        this.setState({ firstName: event.target.value });

    }

    handleChangeLastname(event) {
        this.setState({ lastName: event.target.value });

    }

    handleChangeRelativeFirstName(event) {
        this.setState({ relativeFirstName: event.target.value });
    }

    handleChangeRelativeLastName(event) {
        this.setState({ relativeLastName: event.target.value });
    }

    handleChangeCareNeed(event) {
        this.setState({ careNeed: event.target.value });
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

    render() {

        return (
            <div style={{
                position: 'absolute', left: '50%',
                transform: 'translate(-50%)'
            }}>
                <h1>Borger oplysninger</h1>
                <h3>Ændr oplysninger for borger {this.state.cpr}</h3>
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
                                    <label>Efternavn: {this.state.borger.lastName}</label><br />
                                    <input type="text" onChange={this.handleChangeLastname} ></input><br />
                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>Pårørende:</label><br />
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
                                    <input type="number" onChange={this.handleChangeRelativePhonenumber} min="10000000" max="99999999"></input><br />
                                </td>
                                <td>
                                    <label>Relation</label><br />
                                    <input type="text" onChange={this.handleChangeRelativeRelation} ></input><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Primær pårørende</label><br />
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
                                        <option value="Afklaret">Afklaret</option>
                                        <option value="Uafklaret">Uafklaret</option>
                                        <option value="Revurderingsbehov">Revurderingsbehov</option>
                                    </select><br />
                                </td>
                            </tr>
                            <label></label><br />
                            <tr>
                                <td>
                                    <label>Borgeroverblik:</label><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Mål for ophold: </label><br />
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

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <Button color="primary" onClick={this.handleSubmit}>Gem</Button>
                                </td>
                                <td>
                                    <Link to={{ pathname: "/" }} className="btn btn-primary">Tilbage</Link>
                                </td>
                            </tr>
                        </table>
                    </form>
                </div>
            </div>
        );
    }

    async populateBorgerData() {
        var data;
        await fetch('https://localhost:44356/rccsdb/createcitizen/' + this.state.cpr, {
            method: 'GET',
            credentials: 'include',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("jwt"),
                'Content-Type': 'application/json'
            }
        }).then(response => { data = response.json() });
        //const data = response.json();

        this.setState({ borger: data, loading: false });

        console.log(this.state.borger);
        this.populateStateData();
        //console.log(this.state.firstName);
    }

    populateStateData() {

        let temp = 0
        if (this.props.location.state.type === "Demensbolig") {
            temp = 1
        }

        this.setState({
            type: temp,
            home: this.props.location.state.name,

            firstName: this.state.borger.firstName,
            lastName: this.state.borger.lastName,

            relativeFirstName: this.state.borger.relativeFirstName,
            relativeLastName: this.state.borger.relativeLastName,
            relativePhonenumber: Number(this.state.borger.phonenumber),
            relativeRelation: this.state.borger.relation,
            relativeIsPrimary: this.state.borger.isPrimary,

            startDate: this.state.borger.startDate,
            reevaluationDate: this.state.borger.reevaluationDate,
            plannedDischarge: this.state.borger.plannedDischargeDate,
            prospectiveSituation: this.state.borger.prospectiveSituationStatusForCitizen,
            careNeed: this.state.borger.careNeed,
            purposeOfStay: this.state.borger.purposeOfStay
        });




    }

}
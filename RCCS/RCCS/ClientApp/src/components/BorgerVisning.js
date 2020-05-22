import React, { Component } from 'react';
import { Link } from "react-router-dom";
import '../CSS/StyleSheet.css'

export class BorgerVisning extends Component {
    static displayName = BorgerVisning.name;

    constructor(props) {

        let url = window.location.pathname.split("/");

        super(props);
        this.state = { borger: [], loading: true, cpr: url[2], statusDate: new Date };
    }

    componentDidMount() {
        this.populateBorgerData();
    }

    //DateObjectification()
    //{
    //    const statushist = this.state.borger.statusHistories;
    //    this.setState({ statusDate: new Date(statushist[0].date) });
    //}

    static borgertabel(borger, cpr, statusDate) {
        const relatives = borger.relatives;
        const statushist = borger.progressReports;

        var reports;
        if (statushist.length != 0) {
            reports = statushist[statushist.length - 1].report
        }
        else {
            reports = null;
        }

        console.log(statushist);

        return (
            <table>
                <th>
                    <b>{borger.citizenName}</b>  {borger.age} år {borger.cpr}
                </th>
                <th>  </th>
                <th>
                    <Link to={{ pathname: "/OpdaterBorger/" + borger.cpr }} className="btn btn-primary" color="white">Opdater Borger</Link>
                    <Link to={{ pathname: "/NyRapport/" + borger.cpr }} className="btn btn-primary" color="white">Ny statusrapport</Link>
                </th>
                <tr>
                    <td>
                        <table>
                            <tr>
                                Information
                                
                                </tr>
                            <tr>
                                <b>
                                    Aflastingssted:
                                    </b>

                            </tr>
                            <tr>
                                {borger.respiteCareHome}
                            </tr>
                            <tr>
                                <b>Boligtype:</b>
                            </tr>
                            <tr>
                                {borger.careHomeType}
                            </tr>
                            <tr>
                                <b>Opstartsdato:</b>
                            </tr>
                            <tr>
                                {borger.dateOfAdmission}
                            </tr>
                            <tr>
                                <b>Revurderingsdato:</b>
                            </tr>
                            <tr>
                                {borger.evaluationDate}
                            </tr>
                            <tr>
                                <b>Planlagt udskrivelsesdato:</b>
                            </tr>
                            <tr>
                                {borger.timeUntilDischarge}
                            </tr>
                            <tr>
                                <b>Mål for ophold:</b>
                            </tr>
                            <tr>
                                {borger.purposeOfStay}
                            </tr>
                            <tr>
                                <b>Pårørende:</b>
                            </tr>
                            {relatives.map(relative =>
                                <tr>
                                    <td>{relative.firstName} {relative.lastName}, {relative.phoneNumber}, {relative.relation}  </td>
                                    <td>{relative.isPrimary}</td>
                                </tr>
                            )}
                        </table>
                    </td>
                    <td>
                        <td>
                            <table>
                                <tr>
                                    <b>Overblik</b>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Situation:</b>
                                    </td>
                                    <td>
                                        {borger.purposeOfStay}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Plejebehov:</b>
                                    </td>
                                    <td>
                                        {borger.careNeed}
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Revurderinger:</b>
                                    </td>
                                    <td>
                                        {borger.amountOfEvaluations}
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <b>Statushistorik</b>
                                </tr>
                                <tr>
                                    {statushist.map(statushistory =>
                                        <tr>
                                            <td>{statusDate.toLocaleDateString()} {statushistory.title}</td>
                                        </tr>
                                    )}
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                            </table>
                        </td>
                        <table>
                            <tr>
                                <td>Seneste status rapport</td>
                            </tr>
                            <tr>
                                <td colSpan="4">
                                    <textarea readOnly value={reports} cols="55" rows="6"></textarea>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        );
    }



    //static relativestable(borger) {
    //    const relatives = borger.relatives;
    //    return (
    //        <table className="table table-striped">
    //            <tbody>
    //                {relatives.map(relative =>
    //                    <tr>
    //                        <td>{relative.isPrimary}</td>
    //                        <td>{relative.firstName}</td>
    //                        <td>{relative.lastName}</td>
    //                        <td>{relative.phoneNumber}</td>
    //                        <td>{relative.relation}</td>
    //                    </tr>
    //                )}
    //            </tbody>
    //        </table>
    //    );
    //}



    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : BorgerVisning.borgertabel(this.state.borger, this.state.cpr, this.state.statusDate);


        return (
            <div>
                <h1>Borger oplysninger</h1>
                {contents}
            </div>
        );
    }


    async populateBorgerData() {
        const response = await fetch("https://localhost:44356/rccsdb/CitizenInformation/" + this.state.cpr);
        const data = await response.json();
        this.setState({ borger: data, loading: false });

        const statushist = this.state.borger.progressReports;
        this.setState({ statusDate: new Date(statushist[statushist.length - 1].date) });
        //Ændring
    }

}

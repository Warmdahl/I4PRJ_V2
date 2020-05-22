import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { BrowserRouter as Router, Link, Route } from "react-router-dom";
import './NavMenu.css';

import { Welcome } from './Welcome';
import { LogIn } from './LogIn';
import { FindBorger } from './FindBorger';
import { BoligListe } from './BoligListe';
import { BorgerVisning } from './BorgerVisning';
import { NyRapport } from './NyRapport';
import { OpdaterBorger } from './OpdaterBorger';
import { OpretBorger } from './OpretBorger';
import { Register } from "./Register";
import { ShowAllUsers } from "./ShowAllUsers";
import AdminRoute from "../Routes/AdminRoute";
import CoordinatorRoute from "../Routes/CoordinatorRoute";
import NursingStaffRoute from "../Routes/NursingStaffRoute";

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor (props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar () {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render () {
        return (
            <header>
                <Route>
                    <div>
                        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                            <Container>
                                <NavbarBrand tag={Link} to="/">RCCS: Respite Care Communication System</NavbarBrand>
                                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                                    <ul className="navbar-nav flex-grow">
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/">Welcome</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/log-in">Log In</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/FindBorger">Borger Liste</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/BoligListe">Bolig Liste</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/register">Register </NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/show-all-users">Show all users</NavLink>
                                        </NavItem>
                                    </ul>
                                </Collapse>
                            </Container>
                        </Navbar>
                        <Route exact path="/" component={Welcome} />
                        <Route path="/log-in" component={LogIn} />
                        <Route path='/FindBorger' component={FindBorger} />
                        <Route path='/BoligListe' component={BoligListe} />
                        <Route path='/BorgerVisning' component={BorgerVisning} />
                        <Route path='/NyRapport' component={NyRapport} />
                        <Route path='/OpdaterBorger' component={OpdaterBorger} />
                        <Route path='/OpretBorger' component={OpretBorger} />
                        <AdminRoute path="/Register" component={Register} />
                        <AdminRoute path="/show-all-users" component={ShowAllUsers} />
                        </div>
                    </Route>
            </header>
        );
    }
}

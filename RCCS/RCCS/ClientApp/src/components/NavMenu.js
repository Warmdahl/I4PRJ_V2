import React, {Component} from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {BrowserRouter as Router, Link, Route} from "react-router-dom";
import './NavMenu.css';

import {Welcome} from './Welcome';
import {LogIn} from './LogIn';
import {FindBorger} from './FindBorger';
import {BoligListe} from './BoligListe';
import {BorgerVisning} from './BorgerVisning';
import {NyRapport} from './NyRapport';
import {OpdaterBorger} from './OpdaterBorger';
import {OpretBorger} from './OpretBorger';
import {Register} from "./Register";
import {ShowAllUsers} from "./ShowAllUsers";
import {NotAuthorized} from "./NotAuthorized";
import {LogOut} from "./LogOut";
import {AdminRoute} from "../Security/Routes/AdminRoute";
import {LogInFunction, getLogInState} from "../Security/getLogInState";
import CoordinatorRoute from "../Security/Routes/CoordinatorRoute";
import NursingStaffRoute from "../Security/Routes/NursingStaffRoute";

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {collapsed: true};
        this.state = {userLoggedIn: ""}
    }

    componentDidMount() {
        this.setState ({
            userLoggedIn : getLogInState()
        });
        console.log("NavMenu.js says: componentDidMount and this.state.userLoggedIn is: " + this.state.userLoggedIn)
    }


    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }



    render() {
        let logInGreeting;
        if (this.state.userLoggedIn) {
            logInGreeting = <span className="navbar-text float-left">Logged in</span>
        } else {
            logInGreeting =
                <span className="navbar-text float-left">Ikke logged in</span>
        }
        return (
            <header>
                <Route>
                    <div>
                        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 justify-content-between"
                                light>
                            <Container>
                                {logInGreeting}
                                <NavbarBrand tag={Link} to="/">RCCS: Respite Care Communication System</NavbarBrand>
                                <NavbarToggler onClick={this.toggleNavbar} className="mr-2"/>
                                <Collapse className="d-sm-inline-flex flex-sm-row-reverse"
                                          isOpen={!this.state.collapsed} navbar>
                                    <ul className="navbar-nav flex-grow">
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/">Forside</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/LogIn">Log In</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/FindBorger">Borger
                                                Liste</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/BoligListe">Bolig
                                                Liste</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/LogOut">Log Out </NavLink>
                                        </NavItem>
                                    </ul>
                                </Collapse>
                            </Container>
                        </Navbar>
                        <Route exact path="/" component={Welcome}/>
                        <Route
                            path='/LogIn'
                            render={(props) => <LogIn {...props} userLoggedIn={this.state.UserLoggedIn}/>}
                        />
                        <NursingStaffRoute path='/FindBorger' component={FindBorger}/>
                        <CoordinatorRoute path='/BoligListe' component={BoligListe}/>
                        <CoordinatorRoute path='/BorgerVisning' component={BorgerVisning}/>
                        <NursingStaffRoute path='/NyRapport' component={NyRapport}/>
                        <NursingStaffRoute path='/OpdaterBorger' component={OpdaterBorger}/>
                        <CoordinatorRoute path='/OpretBorger' component={OpretBorger}/>
                        <Route path='/LogOut' component={LogOut}/>
                        <Route path='/NotAuthorized' component={NotAuthorized}/>
                    </div>
                </Route>
            </header>
        );
    }
}

function renderNavItem() {
}

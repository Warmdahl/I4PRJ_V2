import React, {Component} from 'react';
import {getLogInState} from "../Security/SecurityHelperFunctions/getLogInState";

export class LogIn extends Component {
    constructor(props) {
        super(props);
        this.state = {user: [], loading: true, personaleId: null, password: null};
        this.state = {Error: false};
        this.state = {token: null}
        this.handleChange = this.handleChange.bind(this);
        this.handleLogin = this.handleLogin.bind(this);
    }

    handleChange(event) {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({[nam]: val});
    }

    async handleLogin(event) {

        event.preventDefault();

        let user = {
            personaleId: this.state.personaleId,
            password: this.state.password
        };

        let userStringified = JSON.stringify(user);
        const that = this;
        fetch('https://localhost:44356/api/User/login', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: userStringified
        })
            .then(function (response) {
                if (response.ok) {
                    response.json().then(function (data) {
                        console.log("json: " + data);
                        console.log("jwt: " + data["jwt"]);
                        localStorage.setItem("jwt", data["jwt"]);
                        console.log('Logged in');
                        that.setState({
                            UserLoggedIn: true
                        });
                    });
                } else {
                    alert("Fejl i Brugernavn eller password, HTTP-Error: " + response.status);
                    console.log('Error - not logged in');
                    that.setState({
                        Error: true
                    });
                }
            }).catch(error => {
            console.error('Caught error:', error);
            that.setState({
                Error: true
            });
        });
        return user;
    }

    render() {
        const WhatToRender = () => {
            {
                if (getLogInState()) {
                    return <h1>Du er logged in!</h1>
                } else {
                    return (
                        <form onSubmit={this.handleLogin}>
                            <label>
                                Personale Id:
                                <input type="text" name='personaleId' value={this.state.personaleId || ""}
                                       onChange={this.handleChange}/>
                            </label>
                            <label>
                                Password:
                                <input type="password" name='password' value={this.state.password || ""}
                                       onChange={this.handleChange}/>
                            </label>
                            <input type="submit" value="Login"/>
                        </form>
                    );
                }
            }
        }
        return (
            <div>
                {WhatToRender()}
            </div>
        );
    }
}
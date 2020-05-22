import React, { Component } from 'react';

export class Register extends Component {
    constructor(props) {
        super(props);
        this.state = { user: [], loading: true, firstname: '', lastname: '', username: '', password: '' };
        this.state = { userCreatedSuccessfully: false };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    async handleSubmit(event) {
        event.preventDefault();
        let user = {
            firstName: this.state.firstname,
            lastName: this.state.lastname,
            userName: this.state.username,
            password: this.state.password
        };

        let userStringified = JSON.stringify(user);

        const response = await fetch('/users/register', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: userStringified
        });
        if (response.ok) { // if HTTP-status is 200-299
            // get the response body (the method explained below)
            this.setState({
                userCreatedSuccessfully: true
            });
        } else {
            alert("HTTP-Error: " + response.status);
        }
    };


    render() {
        const userCreatedSuccessfully = this.state.userCreatedSuccessfully;
        const WhatToRender = () => {
            {
                if (userCreatedSuccessfully) {
                    return <h1>User created successfully!</h1>
                } else {
                    return (
                        <form onSubmit={this.handleSubmit}>
                            <label>
                                FirstName:
                                <input type="text" name='firstname' value={this.state.firstname || ""}
                                    onChange={this.handleChange} />
                            </label>
                            <label>
                                LastName:
                                <input type="text" name='lastname' value={this.state.lastname || ""}
                                    onChange={this.handleChange} />
                            </label>
                            <label>
                                Username:
                                <input type="text" name='username' value={this.state.username || ""}
                                    onChange={this.handleChange} />
                            </label>
                            <label>
                                Password:
                                <input type="password" name='password' value={this.state.password || ""}
                                    onChange={this.handleChange} />
                            </label>
                            <input type="submit" value="Submit" />
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
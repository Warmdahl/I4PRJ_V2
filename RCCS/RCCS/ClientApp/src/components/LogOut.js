import React, {Component} from 'react';
import {Redirect} from "react-router-dom";

export class LogOut extends Component {
    constructor(props) {
        super(props);
    }


    componentDidMount() {
        console.log("LogOut.js says: Cleared JWT in localStorage");
        localStorage.clear();
    }

    render() {
        return (
            <div>
                <h1>Du er logged ud. Log in her</h1>
            </div>
        );
    }
}

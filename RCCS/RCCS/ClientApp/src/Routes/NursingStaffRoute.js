import React from 'react';
import { Route } from 'react-router-dom';
import { getRole } from "../Security/GetRole";
import Redirect from "react-router-dom/Redirect";

function NursingStaffRoute({ component: Component, ...rest }) {
    let role = getRole();
    switch (role) {
        case "Admin": {
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
            break;
        }
        case "Coordinator": {
            console.log('coordinator privateroute enabled');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
            break;
        }
        case "NursingStaff": {
            console.log('NursingStaff privateroute enabled');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
            break;
        }
        default: {
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Redirect to="log-in" />
                        )
                    }
                />);
            break;
        }
    }
}

export default NursingStaffRoute;
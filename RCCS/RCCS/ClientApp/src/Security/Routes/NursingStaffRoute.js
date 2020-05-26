import React from 'react';
import { Route } from 'react-router-dom';
import { getRole } from "../SecurityHelperFunctions/GetRole";
import Redirect from "react-router-dom/Redirect";

export function NursingStaffRoute({ component: Component, ...rest }) {
    let role = getRole();
    console.log('NursingStaffRoute.js says: NursingStaff route enabled');
    console.log("NursingStaffRoute.js says: Role is: " + role)
    switch (role) {
        case "Admin": {
            console.log('NursingStaffRoute.js says: User is Admin. Access granted.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
        }
        case "Coordinator": {
            console.log('NursingStaffRoute.js says: User is Coordinator. Access granted.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
        }
        case "NursingStaff": {
            console.log('NursingStaffRoute.js says: User is NursingStaff. Access granted.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />);
        }
        default: {
            console.log('NursingStaffRoute.js says: User is not Authorized. Access NOT granted.');
            console.log('Redirected to error page.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Redirect to="NotAuthorized" />
                        )
                    }
                />);
        }
    }
}

export default NursingStaffRoute;
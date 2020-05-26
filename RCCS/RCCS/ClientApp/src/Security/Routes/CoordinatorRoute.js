import React from 'react';
import { Route } from 'react-router-dom';
import { getRole } from "../SecurityHelperFunctions/GetRole";
import Redirect from "react-router-dom/Redirect";

export function CoordinatorRoute({ component: Component, ...rest }) {
    let role = getRole();
    console.log('CoordinatorRoute.js says: Coordinator route enabled');
    console.log("CoordinatorRoute.js says: Role is: " + role)
    switch (role) {
        case "Admin": {
            console.log('CoordinatorRoute.js says: User is Admin. Access granted.');
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
            console.log('CoordinatorRoute.js says: User is Coordinator. Access granted.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Component {...props} />
                        )
                    }
                />)
        }
        default: {
            console.log('CoordinatorRoute.js says: User is not Authorized. Access NOT granted.');
            console.log('Redirected to error page.');
            return (
                <Route
                    {...rest}
                    render={props =>
                        (
                            <Redirect to="NotAuthorized" />
                        )
                    }
                />)
        }
    }
}

export default CoordinatorRoute;
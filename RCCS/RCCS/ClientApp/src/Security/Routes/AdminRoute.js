import React from 'react';
import { Route } from 'react-router-dom';
import { getRole } from "../SecurityHelperFunctions/GetRole";
import Redirect from "react-router-dom/Redirect";

export function AdminRoute({ component: Component, ...rest }) {
    let role = getRole();
    console.log('AdminRoute.js says: Admin route enabled');
    console.log("AdminRoute.js says: Role is: " + role)
    switch (role) {
        case "Admin": {
            console.log('AdminRoute.js says: User is Admin. Access granted.');
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
            console.log('AdminRoute.js says: User is not Authorized. Access NOT granted.');
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

export default AdminRoute;
import React from 'react';
import { Route } from 'react-router-dom';
import { getRole } from "../Security/GetRole";
import Redirect from "react-router-dom/Redirect";

function AdminRoute({ component: Component, ...rest }) {
    let role = getRole();
    console.log("adminroute gets role " + role);
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

export default AdminRoute;
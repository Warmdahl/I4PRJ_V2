import React, {Component} from 'react';

export class NotAuthorized extends Component {
    static displayName = NotAuthorized.name;

    render() {
        return (
            <div>
                <h1>Du har ikke adgang til denne side.</h1>
            </div>
        );
    }
}
import React, { Component } from 'react';
import { App }from './App/App';
export class Layout extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isLoading: true
        };
    }

    componentDidMount() {
        setTimeout(() => {
            this.setState({ isLoading: false });
        }, 2000)
    }

    render() {
        return (
            <App isLoading={this.state.isLoading} />
        );
    }
}
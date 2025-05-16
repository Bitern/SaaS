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
        this.setState({ isLoading: false });
    }

    render() {
        return (
            <App isLoading={this.state.isLoading} />
        );
    }
}
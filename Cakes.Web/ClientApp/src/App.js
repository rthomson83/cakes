import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import {View} from "./components/View";
import {CakeForm} from "./components/CakeForm";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/cake/:id' component={View} />
        <Route path='/new-cake' component={CakeForm} />
      </Layout>
    );
  }
}

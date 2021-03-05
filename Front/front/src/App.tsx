import React from 'react';
import './App.css';
import { useSubscription, gql } from '@apollo/client';

interface Counter {
  count: number;
  updateTime: string;
}

interface Record {
  onRecorded: Counter;
}

const COUNT_SUBSCRIPTION = gql`
  subscription OnRecorded {
    onRecorded {
      count
      updateTime
    }
  }
`;

function Count() {
  const { loading, data } = useSubscription<Record>(COUNT_SUBSCRIPTION);
  return (
    <div>
      <h5>Counter</h5>
      <p>{!loading && data?.onRecorded.count}</p>
      <p>{!loading && data?.onRecorded.updateTime}</p>
    </div>
  );
}

function App() {
  return (
    <div className="App">
      <Count />
    </div>
  );
}

export default App;

import React from 'react'
import { useSubscription, gql } from '@apollo/client'
import { CircularProgress } from '@material-ui/core'

interface Counter {
  count: number;
  recordTime: string;
}

interface Record {
  onRecorded: Counter;
}

const COUNT_SUBSCRIPTION = gql`
  subscription OnRecorded {
    onRecorded {
      count
      recordTime
    }
  }
`;

const Count: React.FC<any> = (props: any) => {
  const { loading, data } = useSubscription<Record>(COUNT_SUBSCRIPTION);
  if (loading) return <CircularProgress />
  return (
    <div>
      <h5>Counter</h5>
      <p>{!loading && data?.onRecorded.count}</p>
      <p>{!loading && data?.onRecorded.recordTime}</p>
    </div>
  );
}

export { Count }
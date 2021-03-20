import React from 'react'
import { useQuery, gql } from '@apollo/client'
import { CircularProgress, Button } from '@material-ui/core'
import { Link } from 'react-router-dom'

interface Counter {
  id: number
  count: number
  recordTime: string
}

const COUNT_QUERY = gql`
  query Counters {
    counters {
      iD
      count
      recordTime
    }
  }
`;

const COUNT_SUBSCRIPTION = gql`
  subscription OnRecorded {
    onRecorded {
      iD
      count
      recordTime
    }
  }
`;

const Count: React.FC<any> = (props: any) => {
  const { loading, error, data, subscribeToMore } = useQuery(COUNT_QUERY)

  React.useEffect(
    () => subscribeToMore({
      document: COUNT_SUBSCRIPTION,
      updateQuery: (prev, { subscriptionData }) => {
        if (!subscriptionData.data) return prev
        const newCount = subscriptionData.data.onRecorded
        return Object.assign({}, prev, {
          counters: [...prev.counters, newCount],
        })
      },
    }),
    [subscribeToMore]
  )

  if (loading) return <CircularProgress />
  if (error) return <p>Got Error...</p>

  const latest = data.counters.slice(-1)[0] as Counter

  return (
    <div>
      <h5>Counter</h5>
      <p>latest count is {latest.count}</p>
      <p>recored at {latest.recordTime}</p>
      <Button variant="contained" component={Link} to="sub">
        Sub
      </Button>
    </div>
  );
}

export { Count }
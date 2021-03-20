import React from 'react'
import { useApolloClient, gql } from '@apollo/client'

const Sub: React.FC<any> = (props: any) => {
    const client = useApolloClient()
    const data = client.readQuery({
      query: gql`
        query GetCounters {
          counters {
            iD
            count
            recordTime
          }
        }
      `,
    })
    if (!data) return <></>
    const latest = data.counters.slice(-1)[0]
    return (
      <div>
        <h5>Sub Page using cache</h5>
        <p>latest count is {latest.count}</p>
        <p>recored at {latest.recordTime}</p>
      </div>
    );
  }
  
  export { Sub }
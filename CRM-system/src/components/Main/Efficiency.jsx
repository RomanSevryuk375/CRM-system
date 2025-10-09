import React from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const data = [
  {
    name: 'ПН',
    uv: 4000,
  },
  {
    name: 'ВТ',
    uv: 3000,
  },
  {
    name: 'СР',
    uv: 2000,
  },
  {
    name: 'ЧТ',
    uv: 2780,
  },
  {
    name: 'ПТ',
    uv: 1890,
  },
  {
    name: 'СБ',
    uv: 2390,
  },
  {
    name: 'ВС',
    uv: 3490,
  },
  
];

const Efficiency = () => {
  return (
      <ResponsiveContainer width="100%" height="100%">
        <BarChart
          width={500}
          height={300}
          data={data}
          margin={{
            top: 16,
            right: 32,
            left: 16,
            bottom: 0,
          }}
        //   barSize={20}
        >
          <XAxis dataKey="name" scale="point" padding={{ left: 50, right: 50 }} />
          <YAxis />
          <Tooltip />
          <Legend />
          <CartesianGrid strokeDasharray="3 3" />
          <Bar dataKey="uv" name='' fill="#0069D0"/>
        </BarChart>
      </ResponsiveContainer>
    );
};

export default Efficiency;
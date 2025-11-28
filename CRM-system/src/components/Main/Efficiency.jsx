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
        style={{ fontFamily: "'Roboto', sans-serif" }}
        width={500}
        height={300}
        data={data}
        margin={{
          top: 16,
          right: 32,
          left: 16,
          bottom: 16,
        }}
      >
        <CartesianGrid strokeDasharray="4 4" />
        <XAxis style={{ fontFamily: "'Roboto', sans-serif" }} dataKey="name" scale="point" padding={{ left: 50, right: 50 }} />
        <YAxis style={{ fontFamily: "'Roboto', sans-serif" }} />
        <Tooltip style={{ fontFamily: "'Roboto', sans-serif" }} />
        <Bar dataKey="uv" name='' fill="#0069D0" />
      </BarChart>
    </ResponsiveContainer>
  );
};

export default Efficiency;
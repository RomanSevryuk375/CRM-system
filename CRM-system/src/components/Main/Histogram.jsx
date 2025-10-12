import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const data = [
  {
    name: 'янв',
    uv: 4000,
    pv: 2400,
  },
  {
    name: 'фев',
    uv: 3000,
    pv: 1398,
  },
  {
    name: 'мар',
    uv: 2000,
    pv: 9800,
  },
  {
    name: 'апр',
    uv: 2780,
    pv: 3908,
  },
  {
    name: 'май',
    uv: 1890,
    pv: 4800,    
  },
  {
    name: 'июн',
    uv: 2390,
    pv: 3800,
  },
  {
    name: 'июл',
    uv: 3490,
    pv: 4300,
  },
  {
    name: 'авг',
    uv: 3000,
    pv: 1398,
  },
  {
    name: 'сен',
    uv: 2000,
    pv: 9800,
  },
  {
    name: 'окт',
    uv: 2780,
    pv: 3908,
  },
  {
    name: 'ноя',
    uv: 1890,
    pv: 4800,
  },
  {
    name: 'дек',
    uv: 2390,
    pv: 3800,
  },
];

const CustomizedAxisTick = ({ x, y, stroke, payload }) => {
  return (
      <g transform={`translate(${x},${y})`}>
        <text x={0} y={0} dy={12} textAnchor="end" fill="#666" transform="rotate(-45)">
          {payload.value}
        </text>
      </g>
    );
};
const Histogram = () => {
  return (
    <ResponsiveContainer width="100%" height="100%">
        <LineChart
        style={{ fontFamily: "'Roboto', sans-serif"}}
          width={500}
          height={300}
          data={data}
          margin={{
            top: 16,
            right: 32,
            left: 16,
            bottom: 0,
          }}
        >
          <CartesianGrid strokeDasharray="4 4" />
          <XAxis dataKey="name" height={50} tick={<CustomizedAxisTick />} />
          <YAxis style={{ fontFamily: "'Roboto', sans-serif"}}/>
          <Tooltip style={{ fontFamily: "'Roboto', sans-serif"}}/>
          <Legend style={{ fontFamily: "'Roboto', sans-serif"}}/>
          <Line type="monotone" name='Прибыль до вычета налогов' dataKey="uv" stroke="#112347" strokeWidth={3} />
          <Line type="monotone" name='Чистая прибыль' dataKey="pv" stroke="#0069D0" strokeWidth={3} />
        </LineChart>
      </ResponsiveContainer>
  );
};

export default Histogram;
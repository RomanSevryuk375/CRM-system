import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const data = [
  {
    name: 'янв',
    uv: 40,
  },
  {
    name: 'фев',
    uv: 30,
  },
  {
    name: 'мар',
    uv: 20,
  },
  {
    name: 'апр',
    uv: 27,
  },
  {
    name: 'май',
    uv: 18, 
  },
  {
    name: 'июн',
    uv: 23,
  },
  {
    name: 'июл',
    uv: 34,
  },
  {
    name: 'авг',
    uv: 30,
  },
  {
    name: 'сен',
    uv: 20,
  },
  {
    name: 'окт',
    uv: 27,
  },
  {
    name: 'ноя',
    uv: 18,
  },
  {
    name: 'дек',
    uv: 23,
  },
];

const CustomizedAxisTick = ({ x, y, stroke, payload }) => {
  return (
      <g transform={`translate(${x},${y})`}>
        <text style={{ fontFamily: "'Roboto', sans-serif" }} x={0} y={0} dy={12} textAnchor="end" fill="#666" transform="rotate(-45)">
          {payload.value}
        </text>
      </g>
    );
};
const HistogramEfficiency = () => {
  return (
    <ResponsiveContainer style={{ fontFamily: "'Roboto', sans-serif" }} width="100%" height="100%">
        <LineChart style={{ fontFamily: "'Roboto', sans-serif" }}
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
          <XAxis style={{ fontFamily: "'Roboto', sans-serif" }} dataKey="name" height={50} tick={<CustomizedAxisTick style={{ fontFamily: "'Roboto', sans-serif" }} />} />
          <YAxis style={{ fontFamily: "'Roboto', sans-serif" }} />
          <Tooltip style={{ fontFamily: "'Roboto', sans-serif" }} />
          <Line style={{ fontFamily: "'Roboto', sans-serif" }} type="monotone" name=' ' dataKey="uv" stroke="#0069D0" strokeWidth={3} />
        </LineChart>
      </ResponsiveContainer>
  );
};

export default HistogramEfficiency;
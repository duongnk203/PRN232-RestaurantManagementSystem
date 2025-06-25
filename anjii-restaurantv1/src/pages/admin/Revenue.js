import React, { useState, useEffect } from 'react';
import { Card, DatePicker, Select, Row, Col, Spin } from 'antd';
import {
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  BarChart,
  Bar,
  Legend,
} from 'recharts';
import { revenueService } from '../../services/revenueService';

const { RangePicker } = DatePicker;
const { Option } = Select;

const Revenue = () => {
  const [dateRange, setDateRange] = useState([]);
  const [viewType, setViewType] = useState('daily');
  const [loading, setLoading] = useState(false);
  const [revenueData, setRevenueData] = useState([]);
  const [topSellingItems, setTopSellingItems] = useState([]);

  const fetchData = async () => {
    try {
      setLoading(true);
      const [revenueStats, topItems] = await Promise.all([
        revenueService.getRevenueStats({
          startDate: dateRange[0]?.format('YYYY-MM-DD'),
          endDate: dateRange[1]?.format('YYYY-MM-DD'),
          groupBy: viewType,
        }),
        revenueService.getTopSellingItems({
          startDate: dateRange[0]?.format('YYYY-MM-DD'),
          endDate: dateRange[1]?.format('YYYY-MM-DD'),
        }),
      ]);

      setRevenueData(revenueStats.data);
      setTopSellingItems(topItems.data);
    } catch (error) {
      console.error('Failed to fetch revenue data:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [dateRange, viewType]);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-6">Revenue Analytics</h1>

      <div className="mb-6 flex gap-4">
        <RangePicker
          onChange={(dates) => setDateRange(dates)}
          className="w-64"
        />
        <Select
          defaultValue="daily"
          onChange={(value) => setViewType(value)}
          className="w-32"
        >
          <Option value="daily">Daily</Option>
          <Option value="weekly">Weekly</Option>
          <Option value="monthly">Monthly</Option>
        </Select>
      </div>

      <Spin spinning={loading}>
        <Row gutter={[16, 16]}>
          <Col span={24}>
            <Card title="Revenue Over Time">
              <div style={{ height: 400 }}>
                <ResponsiveContainer width="100%" height="100%">
                  <AreaChart
                    data={revenueData}
                    margin={{ top: 10, right: 30, left: 0, bottom: 0 }}
                  >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />
                    <Area
                      type="monotone"
                      dataKey="revenue"
                      stroke="#8884d8"
                      fill="#8884d8"
                    />
                  </AreaChart>
                </ResponsiveContainer>
              </div>
            </Card>
          </Col>

          <Col span={12}>
            <Card title="Orders vs Revenue">
              <div style={{ height: 300 }}>
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart
                    data={revenueData}
                    margin={{ top: 10, right: 30, left: 0, bottom: 0 }}
                  >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis yAxisId="left" />
                    <YAxis yAxisId="right" orientation="right" />
                    <Tooltip />
                    <Legend />
                    <Bar
                      yAxisId="left"
                      dataKey="orders"
                      fill="#82ca9d"
                      name="Orders"
                    />
                    <Bar
                      yAxisId="right"
                      dataKey="revenue"
                      fill="#8884d8"
                      name="Revenue"
                    />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </Card>
          </Col>

          <Col span={12}>
            <Card title="Top Selling Items">
              <div style={{ height: 300 }}>
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart
                    data={topSellingItems}
                    margin={{ top: 10, right: 30, left: 0, bottom: 0 }}
                    layout="vertical"
                  >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis type="number" />
                    <YAxis dataKey="name" type="category" />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="quantity" fill="#82ca9d" name="Quantity" />
                    <Bar dataKey="revenue" fill="#8884d8" name="Revenue" />
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </Card>
          </Col>
        </Row>
      </Spin>
    </div>
  );
};

export default Revenue; 
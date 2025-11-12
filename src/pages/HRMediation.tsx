import React from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody, CardFooter } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { CalendarIcon, ClockIcon, UserIcon, FileTextIcon, AlertTriangleIcon, CheckCircleIcon } from 'lucide-react';
export const HRMediation = () => {
  // Mock data for the appeal
  const appeal = {
    id: 'APP-2023-0042',
    employee: 'Michael Johnson',
    employeeId: 'EMP-1234',
    department: 'Finance',
    manager: 'Sarah Williams',
    submittedDate: '2023-10-15',
    status: 'Escalated to HR',
    reason: "I believe my performance rating does not accurately reflect my contributions this quarter. I've consistently exceeded targets in key areas and taken on additional responsibilities not reflected in my evaluation.",
    requestedOutcome: "Review of my performance metrics and adjustment of the rating from 'Meets Expectations' to 'Exceeds Expectations'.",
    managerDecision: "After careful review, I maintain that the 'Meets Expectations' rating is accurate based on the established criteria and department benchmarks.",
    managerComments: "While Michael has performed well in some areas, there are still key development areas that need improvement before reaching the 'Exceeds Expectations' level."
  };
  // Mock appraisal data
  const appraisalData = [{
    category: 'Project Delivery',
    employeeScore: 3,
    managerScore: 2,
    weight: 30,
    hasDiscrepancy: true
  }, {
    category: 'Technical Skills',
    employeeScore: 3,
    managerScore: 2,
    weight: 25,
    hasDiscrepancy: true
  }, {
    category: 'Communication',
    employeeScore: 2,
    managerScore: 2,
    weight: 15,
    hasDiscrepancy: false
  }, {
    category: 'Teamwork',
    employeeScore: 3,
    managerScore: 2,
    weight: 15,
    hasDiscrepancy: true
  }, {
    category: 'Innovation',
    employeeScore: 2,
    managerScore: 1,
    weight: 15,
    hasDiscrepancy: true
  }];
  // Calculate weighted averages
  const employeeWeightedAvg = appraisalData.reduce((acc, item) => acc + item.employeeScore * item.weight / 100, 0).toFixed(2);
  const managerWeightedAvg = appraisalData.reduce((acc, item) => acc + item.managerScore * item.weight / 100, 0).toFixed(2);
  // Mock audit trail
  const auditTrail = [{
    action: 'Appeal Submitted',
    actor: 'Michael Johnson (Employee)',
    timestamp: '2023-10-15 09:23 AM'
  }, {
    action: 'Manager Review',
    actor: 'Sarah Williams (Manager)',
    timestamp: '2023-10-18 02:45 PM'
  }, {
    action: 'Appeal Escalated to HR',
    actor: 'System',
    timestamp: '2023-10-18 02:45 PM'
  }, {
    action: 'HR Review Started',
    actor: 'Jessica Thompson (HR)',
    timestamp: '2023-10-19 10:30 AM'
  }];
  return <Layout title="HR Mediation" subtitle="Review and mediate unresolved appraisal appeals">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2">
          <Card>
            <CardHeader>
              <div className="flex justify-between items-center">
                <div>
                  <h2 className="text-lg font-medium text-gray-900">
                    Appeal #{appeal.id}
                  </h2>
                  <div className="mt-1 flex items-center">
                    <Badge variant="primary">{appeal.status}</Badge>
                    <span className="ml-2 text-sm text-gray-500">
                      Submitted on {appeal.submittedDate}
                    </span>
                  </div>
                </div>
                <Button variant="outline" size="sm">
                  View Full History
                </Button>
              </div>
            </CardHeader>
            <CardBody>
              <div className="space-y-6">
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Employee Information
                  </h3>
                  <div className="mt-2 grid grid-cols-1 sm:grid-cols-2 gap-4">
                    <div className="flex items-center space-x-2">
                      <UserIcon className="h-4 w-4 text-gray-400" />
                      <span className="text-sm text-gray-900">
                        {appeal.employee} ({appeal.employeeId})
                      </span>
                    </div>
                    <div className="flex items-center space-x-2">
                      <FileTextIcon className="h-4 w-4 text-gray-400" />
                      <span className="text-sm text-gray-900">
                        Department: {appeal.department}
                      </span>
                    </div>
                  </div>
                </div>
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Appeal Reason
                  </h3>
                  <p className="mt-2 text-sm text-gray-700">{appeal.reason}</p>
                </div>
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Requested Outcome
                  </h3>
                  <p className="mt-2 text-sm text-gray-700">
                    {appeal.requestedOutcome}
                  </p>
                </div>
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Manager's Decision
                  </h3>
                  <p className="mt-2 text-sm text-gray-700">
                    {appeal.managerDecision}
                  </p>
                </div>
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Manager's Comments
                  </h3>
                  <p className="mt-2 text-sm text-gray-700">
                    {appeal.managerComments}
                  </p>
                </div>
                <div>
                  <h3 className="text-sm font-medium text-gray-500">
                    Performance Scoring
                  </h3>
                  <div className="mt-2 overflow-x-auto">
                    <table className="min-w-full divide-y divide-gray-200">
                      <thead className="bg-gray-50">
                        <tr>
                          <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Category
                          </th>
                          <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Weight
                          </th>
                          <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Employee Score
                          </th>
                          <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Manager Score
                          </th>
                          <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Discrepancy
                          </th>
                        </tr>
                      </thead>
                      <tbody className="bg-white divide-y divide-gray-200">
                        {appraisalData.map((item, index) => <tr key={index}>
                            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                              {item.category}
                            </td>
                            <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                              {item.weight}%
                            </td>
                            <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                              {item.employeeScore}
                            </td>
                            <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                              {item.managerScore}
                            </td>
                            <td className="px-6 py-4 whitespace-nowrap">
                              {item.hasDiscrepancy ? <Badge variant="warning">Discrepancy</Badge> : <Badge variant="success">Aligned</Badge>}
                            </td>
                          </tr>)}
                        <tr className="bg-gray-50">
                          <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                            Weighted Average
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                            100%
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-primary">
                            {employeeWeightedAvg}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-primary">
                            {managerWeightedAvg}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap">
                            {Number(employeeWeightedAvg) - Number(managerWeightedAvg) > 0.5 ? <Badge variant="danger">Significant</Badge> : <Badge variant="warning">Minor</Badge>}
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                  <div className="mt-2 text-xs text-gray-500">
                    <p>
                      Score Legend: 1 = Below Expectations, 2 = Meets
                      Expectations, 3 = Exceeds Expectations
                    </p>
                  </div>
                </div>
              </div>
            </CardBody>
          </Card>
          <div className="mt-6">
            <Card>
              <CardHeader>
                <h2 className="text-lg font-medium text-gray-900">
                  HR Mediation Decision
                </h2>
              </CardHeader>
              <CardBody>
                <div className="space-y-6">
                  <div>
                    <label htmlFor="decision" className="block text-sm font-medium text-gray-700">
                      Final Decision
                    </label>
                    <div className="mt-1">
                      <textarea id="decision" name="decision" rows={4} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Enter your final decision (max 500 characters)" maxLength={500} />
                    </div>
                  </div>
                  <div className="flex flex-col sm:flex-row sm:justify-between space-y-4 sm:space-y-0">
                    <div className="flex space-x-3">
                      <Button variant="primary">Support Employee Appeal</Button>
                      <Button variant="secondary">
                        Support Manager Decision
                      </Button>
                    </div>
                    <Button variant="outline">
                      Request Additional Information
                    </Button>
                  </div>
                </div>
              </CardBody>
            </Card>
          </div>
        </div>
        <div className="lg:col-span-1 space-y-6">
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">
                Schedule Mediation Meeting
              </h2>
            </CardHeader>
            <CardBody>
              <div className="space-y-4">
                <div className="bg-gray-50 p-4 rounded-md">
                  <div className="flex justify-center">
                    <CalendarIcon className="h-12 w-12 text-primary" />
                  </div>
                  <p className="mt-2 text-center text-sm text-gray-600">
                    Select a date and time to schedule a mediation meeting with
                    the employee and manager.
                  </p>
                </div>
                <div>
                  <label htmlFor="date" className="block text-sm font-medium text-gray-700">
                    Date
                  </label>
                  <div className="mt-1">
                    <input type="date" name="date" id="date" className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" />
                  </div>
                </div>
                <div>
                  <label htmlFor="time" className="block text-sm font-medium text-gray-700">
                    Time
                  </label>
                  <div className="mt-1">
                    <input type="time" name="time" id="time" className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" />
                  </div>
                </div>
                <div>
                  <label htmlFor="duration" className="block text-sm font-medium text-gray-700">
                    Duration
                  </label>
                  <div className="mt-1">
                    <select id="duration" name="duration" className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md">
                      <option value="30">30 minutes</option>
                      <option value="45">45 minutes</option>
                      <option value="60">60 minutes</option>
                      <option value="90">90 minutes</option>
                    </select>
                  </div>
                </div>
                <div>
                  <label htmlFor="participants" className="block text-sm font-medium text-gray-700">
                    Participants
                  </label>
                  <div className="mt-1">
                    <div className="flex items-center space-x-2 mb-2">
                      <input id="employee" name="employee" type="checkbox" className="h-4 w-4 text-primary focus:ring-primary border-gray-300 rounded" defaultChecked />
                      <label htmlFor="employee" className="text-sm text-gray-700">
                        Employee (Michael Johnson)
                      </label>
                    </div>
                    <div className="flex items-center space-x-2 mb-2">
                      <input id="manager" name="manager" type="checkbox" className="h-4 w-4 text-primary focus:ring-primary border-gray-300 rounded" defaultChecked />
                      <label htmlFor="manager" className="text-sm text-gray-700">
                        Manager (Sarah Williams)
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <input id="hr" name="hr" type="checkbox" className="h-4 w-4 text-primary focus:ring-primary border-gray-300 rounded" defaultChecked />
                      <label htmlFor="hr" className="text-sm text-gray-700">
                        HR Representative (You)
                      </label>
                    </div>
                  </div>
                </div>
              </div>
            </CardBody>
            <CardFooter>
              <Button variant="primary" fullWidth>
                Schedule Meeting
              </Button>
            </CardFooter>
          </Card>
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">Audit Trail</h2>
            </CardHeader>
            <CardBody className="p-0">
              <div className="divide-y divide-gray-200">
                {auditTrail.map((item, index) => <div key={index} className="p-4">
                    <div className="flex items-start">
                      <div className="flex-shrink-0">
                        <div className="h-8 w-8 rounded-full bg-gray-100 flex items-center justify-center">
                          {item.action.includes('Submitted') ? <FileTextIcon className="h-4 w-4 text-gray-600" /> : item.action.includes('Escalated') ? <AlertTriangleIcon className="h-4 w-4 text-yellow-600" /> : item.action.includes('Started') ? <CheckCircleIcon className="h-4 w-4 text-primary" /> : <ClockIcon className="h-4 w-4 text-gray-600" />}
                        </div>
                      </div>
                      <div className="ml-3 flex-1">
                        <p className="text-sm font-medium text-gray-900">
                          {item.action}
                        </p>
                        <div className="mt-1 flex items-center text-xs text-gray-500">
                          <span>{item.actor}</span>
                          <span className="mx-1">•</span>
                          <span>{item.timestamp}</span>
                        </div>
                      </div>
                    </div>
                  </div>)}
              </div>
            </CardBody>
          </Card>
        </div>
      </div>
    </Layout>;
};
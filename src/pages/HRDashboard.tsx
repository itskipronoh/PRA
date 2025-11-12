import React, { useState, Fragment } from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { AlertTriangleIcon, CheckCircleIcon, FileTextIcon, TrendingUpIcon, LockIcon } from 'lucide-react';
export const HRDashboard = () => {
  const [selectedAppraisal, setSelectedAppraisal] = useState<number | null>(null);
  const [returnComment, setReturnComment] = useState('');
  const reviewCycleInfo = {
    currentCycle: 'H2 2023',
    period: 'Jul-Dec 2023',
    reviewWindow: 'Dec 1-15, 2023',
    nextCycle: 'H1 2024 (Jan-Jun)'
  };
  const pendingAppraisals = [{
    id: 1,
    employeeName: 'Michael Johnson',
    employeeId: 'EMP-1234',
    department: 'Finance',
    manager: 'Sarah Williams',
    submittedDate: '2023-12-15',
    weightedScore: 83.5,
    rating: 'Exceptional',
    hasDiscrepancy: true,
    discrepancyDetails: 'Score vs. comments mismatch in Core Competencies',
    reviewLocked: true
  }, {
    id: 2,
    employeeName: 'Emily Chen',
    employeeId: 'EMP-2345',
    department: 'Operations',
    manager: 'Robert Brown',
    submittedDate: '2023-12-14',
    weightedScore: 72.3,
    rating: 'Satisfactory',
    hasDiscrepancy: false,
    discrepancyDetails: null,
    reviewLocked: true
  }, {
    id: 3,
    employeeName: 'David Martinez',
    employeeId: 'EMP-3456',
    department: 'IT',
    manager: 'Lisa Anderson',
    submittedDate: '2023-12-13',
    weightedScore: 48.2,
    rating: 'Unacceptable',
    hasDiscrepancy: true,
    discrepancyDetails: 'Significant gap between employee and manager scores',
    reviewLocked: true
  }];
  const fairnessMetrics = {
    departments: [{
      name: 'Finance',
      avgScore: 78.5,
      count: 12
    }, {
      name: 'Operations',
      avgScore: 71.2,
      count: 15
    }, {
      name: 'IT',
      avgScore: 75.8,
      count: 10
    }, {
      name: 'HR',
      avgScore: 73.4,
      count: 8
    }, {
      name: 'Sales',
      avgScore: 76.9,
      count: 14
    }],
    overallAverage: 75.2,
    performanceDistribution: {
      exceptional: 18,
      satisfactory: 32,
      average: 7,
      unacceptable: 2
    }
  };
  const handleApprove = (id: number) => {
    alert(`Appraisal #${id} approved and finalized. Review is now locked. Employee and manager have been notified.`);
  };
  const handleReturn = (id: number) => {
    if (!returnComment.trim()) {
      alert('Please provide comments explaining why the appraisal is being returned');
      return;
    }
    alert(`Appraisal #${id} unlocked and returned to manager with comments. Manager has been notified.`);
    setSelectedAppraisal(null);
    setReturnComment('');
  };
  const handleUnlockReview = (id: number) => {
    const confirmed = window.confirm('Are you sure you want to unlock this finalized review? This action should only be taken when necessary.');
    if (confirmed) {
      alert(`Review #${id} has been unlocked and can now be edited by the manager.`);
    }
  };
  const getPerformanceColor = (rating: string) => {
    switch (rating) {
      case 'Exceptional':
        return 'success';
      case 'Satisfactory':
        return 'primary';
      case 'Average':
        return 'warning';
      case 'Unacceptable':
        return 'danger';
      default:
        return 'default';
    }
  };
  return <Layout title="HR Appraisal Approval Dashboard" subtitle={`Review and approve completed appraisals for ${reviewCycleInfo.currentCycle}`}>
      <div className="mb-6">
        <Card>
          <CardBody>
            <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
              <div>
                <p className="text-xs text-gray-500">Current Review Cycle</p>
                <p className="text-sm font-medium text-gray-900">
                  {reviewCycleInfo.currentCycle}
                </p>
                <p className="text-xs text-gray-500">
                  {reviewCycleInfo.period}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-500">Review Window</p>
                <p className="text-sm font-medium text-gray-900">
                  {reviewCycleInfo.reviewWindow}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-500">Next Cycle</p>
                <p className="text-sm font-medium text-gray-900">
                  {reviewCycleInfo.nextCycle}
                </p>
              </div>
              <div>
                <p className="text-xs text-gray-500">Overall Average Score</p>
                <p className="text-2xl font-bold text-primary">
                  {fairnessMetrics.overallAverage}%
                </p>
              </div>
            </div>
          </CardBody>
        </Card>
      </div>
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2 space-y-6">
          <Card>
            <CardHeader>
              <div className="flex justify-between items-center">
                <h2 className="text-lg font-medium text-gray-900">
                  Pending Appraisals
                </h2>
                <Badge variant="warning">
                  {pendingAppraisals.length} Pending
                </Badge>
              </div>
            </CardHeader>
            <CardBody className="p-0">
              <div className="overflow-x-auto">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead className="bg-gray-50">
                    <tr>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                        Employee
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                        Department
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                        Score & Rating
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                        Status
                      </th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                        Actions
                      </th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    {pendingAppraisals.map(appraisal => <Fragment key={appraisal.id}>
                        <tr className={selectedAppraisal === appraisal.id ? 'bg-blue-50' : ''}>
                          <td className="px-6 py-4 whitespace-nowrap">
                            <div className="text-sm font-medium text-gray-900">
                              {appraisal.employeeName}
                            </div>
                            <div className="text-xs text-gray-500">
                              {appraisal.employeeId}
                            </div>
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                            {appraisal.department}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap">
                            <div className="text-sm font-bold text-gray-900">
                              {appraisal.weightedScore}%
                            </div>
                            <Badge variant={getPerformanceColor(appraisal.rating) as any}>
                              {appraisal.rating}
                            </Badge>
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap">
                            <div className="flex flex-col space-y-1">
                              {appraisal.hasDiscrepancy ? <Badge variant="warning">
                                  <AlertTriangleIcon className="h-3 w-3 mr-1" />
                                  Flagged
                                </Badge> : <Badge variant="success">
                                  <CheckCircleIcon className="h-3 w-3 mr-1" />
                                  Clear
                                </Badge>}
                              {appraisal.reviewLocked && <Badge variant="default">
                                  <LockIcon className="h-3 w-3 mr-1" />
                                  Locked
                                </Badge>}
                            </div>
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm">
                            <div className="flex space-x-2">
                              <Button size="sm" variant="outline" onClick={() => setSelectedAppraisal(selectedAppraisal === appraisal.id ? null : appraisal.id)}>
                                {selectedAppraisal === appraisal.id ? 'Hide' : 'Review'}
                              </Button>
                              <Button size="sm" variant="primary" onClick={() => handleApprove(appraisal.id)}>
                                Approve
                              </Button>
                            </div>
                          </td>
                        </tr>
                        {selectedAppraisal === appraisal.id && <tr>
                            <td colSpan={5} className="px-6 py-4 bg-gray-50">
                              <div className="space-y-4">
                                <div>
                                  <h4 className="text-sm font-medium text-gray-900 mb-2">
                                    Appraisal Details
                                  </h4>
                                  <div className="grid grid-cols-2 gap-4 text-sm">
                                    <div>
                                      <span className="text-gray-500">
                                        Manager:
                                      </span>
                                      <span className="ml-2 text-gray-900">
                                        {appraisal.manager}
                                      </span>
                                    </div>
                                    <div>
                                      <span className="text-gray-500">
                                        Submitted:
                                      </span>
                                      <span className="ml-2 text-gray-900">
                                        {appraisal.submittedDate}
                                      </span>
                                    </div>
                                    <div>
                                      <span className="text-gray-500">
                                        Weighted Score:
                                      </span>
                                      <span className="ml-2 text-gray-900 font-bold">
                                        {appraisal.weightedScore}%
                                      </span>
                                    </div>
                                    <div>
                                      <span className="text-gray-500">
                                        Performance Standard:
                                      </span>
                                      <Badge variant={getPerformanceColor(appraisal.rating) as any} className="ml-2">
                                        {appraisal.rating}
                                      </Badge>
                                    </div>
                                  </div>
                                </div>
                                {appraisal.hasDiscrepancy && <div className="bg-yellow-50 border border-yellow-200 rounded-md p-3">
                                    <div className="flex items-start">
                                      <AlertTriangleIcon className="h-5 w-5 text-yellow-600 mr-2 mt-0.5" />
                                      <div>
                                        <p className="text-sm font-medium text-yellow-900">
                                          Discrepancy Detected
                                        </p>
                                        <p className="text-sm text-yellow-700">
                                          {appraisal.discrepancyDetails}
                                        </p>
                                      </div>
                                    </div>
                                  </div>}
                                {appraisal.reviewLocked && <div className="bg-blue-50 border border-blue-200 rounded-md p-3">
                                    <div className="flex items-start justify-between">
                                      <div className="flex items-start">
                                        <LockIcon className="h-5 w-5 text-blue-600 mr-2 mt-0.5" />
                                        <div>
                                          <p className="text-sm font-medium text-blue-900">
                                            Review Locked
                                          </p>
                                          <p className="text-sm text-blue-700">
                                            This review has been finalized and
                                            is locked. Only HR can unlock it for
                                            edits.
                                          </p>
                                        </div>
                                      </div>
                                      <Button size="sm" variant="outline" onClick={() => handleUnlockReview(appraisal.id)}>
                                        Unlock Review
                                      </Button>
                                    </div>
                                  </div>}
                                <div>
                                  <label className="block text-sm font-medium text-gray-700 mb-2">
                                    Return Comments (if returning to manager)
                                  </label>
                                  <textarea value={returnComment} onChange={e => setReturnComment(e.target.value)} rows={3} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Explain why this appraisal needs revision" />
                                </div>
                                <div className="flex space-x-3">
                                  <Button variant="secondary" onClick={() => handleReturn(appraisal.id)}>
                                    Return to Manager
                                  </Button>
                                  <Button variant="outline">
                                    View Full Appraisal
                                  </Button>
                                  <Button variant="outline">
                                    Generate Report
                                  </Button>
                                </div>
                              </div>
                            </td>
                          </tr>}
                      </Fragment>)}
                  </tbody>
                </table>
              </div>
            </CardBody>
          </Card>
        </div>
        <div className="lg:col-span-1 space-y-6">
          <Card>
            <CardHeader>
              <div className="flex items-center">
                <TrendingUpIcon className="h-5 w-5 text-primary mr-2" />
                <h2 className="text-lg font-medium text-gray-900">
                  Performance Distribution
                </h2>
              </div>
            </CardHeader>
            <CardBody>
              <div className="space-y-4">
                <div className="space-y-3">
                  <div className="flex justify-between items-center">
                    <div className="flex items-center">
                      <div className="w-3 h-3 rounded-full bg-green-500 mr-2"></div>
                      <span className="text-sm text-gray-700">
                        Exceptional (80%+)
                      </span>
                    </div>
                    <span className="text-sm font-medium text-gray-900">
                      {fairnessMetrics.performanceDistribution.exceptional}
                    </span>
                  </div>
                  <div className="flex justify-between items-center">
                    <div className="flex items-center">
                      <div className="w-3 h-3 rounded-full bg-blue-500 mr-2"></div>
                      <span className="text-sm text-gray-700">
                        Satisfactory (60-79%)
                      </span>
                    </div>
                    <span className="text-sm font-medium text-gray-900">
                      {fairnessMetrics.performanceDistribution.satisfactory}
                    </span>
                  </div>
                  <div className="flex justify-between items-center">
                    <div className="flex items-center">
                      <div className="w-3 h-3 rounded-full bg-yellow-500 mr-2"></div>
                      <span className="text-sm text-gray-700">
                        Average (50-59%)
                      </span>
                    </div>
                    <span className="text-sm font-medium text-gray-900">
                      {fairnessMetrics.performanceDistribution.average}
                    </span>
                  </div>
                  <div className="flex justify-between items-center">
                    <div className="flex items-center">
                      <div className="w-3 h-3 rounded-full bg-red-500 mr-2"></div>
                      <span className="text-sm text-gray-700">
                        Unacceptable (&lt;50%)
                      </span>
                    </div>
                    <span className="text-sm font-medium text-gray-900">
                      {fairnessMetrics.performanceDistribution.unacceptable}
                    </span>
                  </div>
                </div>
              </div>
            </CardBody>
          </Card>
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">
                Department Comparison
              </h2>
            </CardHeader>
            <CardBody>
              <div className="space-y-3">
                {fairnessMetrics.departments.map(dept => {
                const variance = ((dept.avgScore - fairnessMetrics.overallAverage) / fairnessMetrics.overallAverage * 100).toFixed(1);
                const isAbove = dept.avgScore > fairnessMetrics.overallAverage;
                return <div key={dept.name} className="border-b border-gray-200 pb-2">
                      <div className="flex justify-between items-center mb-1">
                        <span className="text-sm text-gray-700">
                          {dept.name}
                        </span>
                        <span className="text-sm font-medium text-gray-900">
                          {dept.avgScore}%
                        </span>
                      </div>
                      <div className="flex items-center justify-between">
                        <div className="w-full bg-gray-200 rounded-full h-2 mr-2">
                          <div className="bg-primary h-2 rounded-full" style={{
                        width: `${dept.avgScore / 100 * 100}%`
                      }} />
                        </div>
                        <span className={`text-xs ${isAbove ? 'text-green-600' : 'text-red-600'}`}>
                          {isAbove ? '+' : ''}
                          {variance}%
                        </span>
                      </div>
                      <p className="text-xs text-gray-500 mt-1">
                        {dept.count} employees
                      </p>
                    </div>;
              })}
              </div>
            </CardBody>
          </Card>
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">Quick Stats</h2>
            </CardHeader>
            <CardBody>
              <div className="space-y-3">
                <div className="flex justify-between items-center">
                  <span className="text-sm text-gray-700">
                    Total Appraisals
                  </span>
                  <span className="text-sm font-medium text-gray-900">59</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-sm text-gray-700">Approved</span>
                  <span className="text-sm font-medium text-green-600">56</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-sm text-gray-700">Pending</span>
                  <span className="text-sm font-medium text-yellow-600">3</span>
                </div>
                <div className="flex justify-between items-center">
                  <span className="text-sm text-gray-700">Flagged</span>
                  <span className="text-sm font-medium text-red-600">2</span>
                </div>
              </div>
            </CardBody>
          </Card>
        </div>
      </div>
    </Layout>;
};
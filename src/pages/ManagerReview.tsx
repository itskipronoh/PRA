import React, { useState } from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody, CardFooter } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { AlertTriangleIcon, CheckCircleIcon, SendIcon, LockIcon, UsersIcon } from 'lucide-react';
export const ManagerReview = () => {
  const reviewCycleInfo = {
    cycle: 'H2 2023',
    period: 'Jul-Dec 2023',
    reviewWindow: 'Dec 1-15, 2023',
    goalsLocked: true
  };
  const employeeAssessment = {
    employeeName: 'Michael Johnson',
    employeeId: 'EMP-1234',
    department: 'Finance',
    period: 'H2 2023',
    sections: [{
      id: 1,
      title: 'Goal Achievement',
      weight: 70,
      goalText: 'Increase quarterly sales revenue by 15% and complete digital transformation project',
      employeeActualPerformance: 'Achieved 18% increase in quarterly sales revenue by implementing new client acquisition strategies. Successfully completed digital transformation project 2 weeks ahead of schedule.',
      employeeScore: 3,
      managerScore: null,
      managerComments: '',
      peerFeedback: "Michael has been instrumental in driving our team's success this quarter."
    }, {
      id: 2,
      title: 'Compliance & Learning',
      weight: 20,
      goalText: 'Complete all mandatory compliance training and achieve 100% on assessments',
      employeeActualPerformance: 'Completed all mandatory compliance training modules ahead of schedule and achieved 100% on all assessment tests. Also completed 3 additional optional courses.',
      employeeScore: 3,
      managerScore: null,
      managerComments: '',
      peerFeedback: 'Always compliant and proactive about learning new regulations.'
    }, {
      id: 3,
      title: 'Core Competencies',
      weight: 10,
      goalText: 'Demonstrate leadership, teamwork, and effective communication skills',
      employeeActualPerformance: 'Led cross-functional project teams and received positive feedback from stakeholders on communication and collaboration skills. Mentored 2 junior team members.',
      employeeScore: 3,
      managerScore: null,
      managerComments: '',
      peerFeedback: 'Excellent communicator and team player.'
    }]
  };
  const [sections, setSections] = useState(employeeAssessment.sections);
  const [signature, setSignature] = useState('');
  const [hrNotes, setHrNotes] = useState('');
  const handleManagerScoreChange = (id: number, score: number) => {
    setSections(sections.map(section => section.id === id ? {
      ...section,
      managerScore: score
    } : section));
  };
  const handleManagerCommentsChange = (id: number, value: string) => {
    setSections(sections.map(section => section.id === id ? {
      ...section,
      managerComments: value
    } : section));
  };
  const calculateWeightedAverage = (scoreType: 'employee' | 'manager') => {
    const total = sections.reduce((acc, section) => {
      const score = scoreType === 'employee' ? section.employeeScore : section.managerScore;
      if (score !== null) {
        return acc + score * section.weight / 100;
      }
      return acc;
    }, 0);
    return (total * 100 / 3).toFixed(1);
  };
  const getPerformanceStandard = (score: number) => {
    if (score < 50) return {
      label: 'Unacceptable',
      color: 'danger'
    };
    if (score < 60) return {
      label: 'Average',
      color: 'warning'
    };
    if (score < 80) return {
      label: 'Satisfactory',
      color: 'primary'
    };
    return {
      label: 'Exceptional',
      color: 'success'
    };
  };
  const hasDiscrepancies = sections.some(section => section.managerScore !== null && Math.abs(section.employeeScore - section.managerScore) >= 1);
  const handleSubmit = () => {
    const allScored = sections.every(section => section.managerScore !== null);
    if (!allScored) {
      alert('Please provide scores for all sections');
      return;
    }
    if (!signature.trim()) {
      alert('Please provide your digital signature');
      return;
    }
    alert('Performance assessment submitted successfully. Employee and HR have been notified.');
  };
  const employeeWeightedAvg = parseFloat(calculateWeightedAverage('employee'));
  const managerWeightedAvg = parseFloat(calculateWeightedAverage('manager'));
  const employeeStandard = getPerformanceStandard(employeeWeightedAvg);
  const managerStandard = managerWeightedAvg ? getPerformanceStandard(managerWeightedAvg) : null;
  return <Layout title="Manager Performance Review" subtitle="Review and assess employee performance with multi-perspective evaluation">
      <div className="space-y-6">
        <Card>
          <CardHeader>
            <div className="flex justify-between items-start">
              <div>
                <h2 className="text-lg font-medium text-gray-900">
                  {employeeAssessment.employeeName} (
                  {employeeAssessment.employeeId})
                </h2>
                <p className="text-sm text-gray-500">
                  {employeeAssessment.department} • Review Cycle:{' '}
                  {reviewCycleInfo.cycle} ({reviewCycleInfo.period})
                </p>
              </div>
              <div className="flex items-center space-x-2">
                {hasDiscrepancies && <Badge variant="warning">
                    <AlertTriangleIcon className="h-3 w-3 mr-1" />
                    Discrepancies Detected
                  </Badge>}
                {reviewCycleInfo.goalsLocked && <Badge variant="default">
                    <LockIcon className="h-3 w-3 mr-1" />
                    Goals Locked
                  </Badge>}
              </div>
            </div>
          </CardHeader>
          <CardBody>
            <div className="space-y-8">
              {sections.map(section => {
              const hasDiscrepancy = section.managerScore !== null && Math.abs(section.employeeScore - section.managerScore) >= 1;
              return <div key={section.id} className={`border rounded-lg p-6 ${hasDiscrepancy ? 'border-yellow-300 bg-yellow-50' : 'border-gray-200'}`}>
                    <div className="flex justify-between items-start mb-4">
                      <div>
                        <h3 className="text-base font-medium text-gray-900">
                          {section.title}
                        </h3>
                      </div>
                      <div className="flex items-center space-x-2">
                        <Badge variant="default">
                          Weight: {section.weight}%
                        </Badge>
                        {hasDiscrepancy && <Badge variant="warning">
                            <AlertTriangleIcon className="h-3 w-3 mr-1" />
                            Score Discrepancy
                          </Badge>}
                      </div>
                    </div>
                    <div className="mb-4">
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Goal/Competency (Locked for Review Period)
                      </label>
                      <div className="bg-gray-50 p-3 rounded-md border border-gray-200">
                        <div className="flex items-start">
                          <LockIcon className="h-4 w-4 text-gray-400 mr-2 mt-0.5" />
                          <p className="text-sm text-gray-700">
                            {section.goalText}
                          </p>
                        </div>
                      </div>
                    </div>
                    <div className="mb-4">
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Employee Self-Assessment
                      </label>
                      <div className="bg-blue-50 p-3 rounded-md">
                        <p className="text-sm text-gray-700">
                          {section.employeeActualPerformance}
                        </p>
                        <div className="mt-2 flex items-center">
                          <span className="text-xs font-medium text-gray-500 mr-2">
                            Employee Score:
                          </span>
                          <Badge variant="primary">
                            {section.employeeScore}
                          </Badge>
                        </div>
                      </div>
                    </div>
                    <div className="mb-4">
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        <UsersIcon className="h-4 w-4 inline mr-1" />
                        Peer Feedback
                      </label>
                      <div className="bg-green-50 p-3 rounded-md">
                        <p className="text-sm text-gray-700 italic">
                          {section.peerFeedback}
                        </p>
                      </div>
                    </div>
                    <div className="mb-4">
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Manager Score *
                      </label>
                      <div className="flex space-x-4">
                        {[1, 2, 3].map(score => <button key={score} onClick={() => handleManagerScoreChange(section.id, score)} className={`flex-1 py-3 px-4 border rounded-md text-sm font-medium transition-colors ${section.managerScore === score ? 'bg-primary text-white border-primary' : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50'}`}>
                            {score === 1 && '1 - Below'}
                            {score === 2 && '2 - Meets'}
                            {score === 3 && '3 - Exceeds'}
                          </button>)}
                      </div>
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">
                        Manager Comments *
                      </label>
                      <textarea value={section.managerComments} onChange={e => handleManagerCommentsChange(section.id, e.target.value)} rows={3} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Provide feedback and justification for your score" />
                    </div>
                  </div>;
            })}
            </div>
            <div className="mt-8 border-t border-gray-200 pt-6">
              <h3 className="text-base font-medium text-gray-900 mb-4">
                Performance Summary
              </h3>
              <div className="bg-gradient-to-r from-blue-50 to-indigo-50 p-6 rounded-lg">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                  <div>
                    <p className="text-sm text-gray-600 mb-2">
                      Employee Self-Assessment
                    </p>
                    <p className="text-3xl font-bold text-primary mb-1">
                      {employeeWeightedAvg}%
                    </p>
                    <Badge variant={employeeStandard.color as any}>
                      {employeeStandard.label}
                    </Badge>
                  </div>
                  <div>
                    <p className="text-sm text-gray-600 mb-2">
                      Manager Assessment
                    </p>
                    <p className="text-3xl font-bold text-secondary mb-1">
                      {managerWeightedAvg ? `${managerWeightedAvg}%` : 'N/A'}
                    </p>
                    {managerStandard && <Badge variant={managerStandard.color as any}>
                        {managerStandard.label}
                      </Badge>}
                  </div>
                </div>
                <div className="mt-4 pt-4 border-t border-gray-200">
                  <p className="text-xs text-gray-600">
                    Scoring: Below 50% = Unacceptable | 50-59% = Average |
                    60-79% = Satisfactory | 80%+ = Exceptional
                  </p>
                </div>
              </div>
            </div>
            <div className="mt-6 border-t border-gray-200 pt-6">
              <label className="block text-sm font-medium text-gray-700 mb-2">
                HR Notes (Optional - for HR review)
              </label>
              <textarea value={hrNotes} onChange={e => setHrNotes(e.target.value)} rows={3} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Add any notes for HR consideration" />
            </div>
            <div className="mt-6 border-t border-gray-200 pt-6">
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Digital Signature *
              </label>
              <input type="text" value={signature} onChange={e => setSignature(e.target.value)} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Type your full name to sign" />
              <p className="mt-1 text-xs text-gray-500">
                By signing, you confirm this review is complete and accurate.
                Once submitted, this review will be locked unless reopened by
                HR.
              </p>
            </div>
          </CardBody>
          <CardFooter>
            <div className="flex justify-end w-full">
              <Button variant="primary" onClick={handleSubmit}>
                <SendIcon className="h-4 w-4 mr-2" />
                Submit Assessment
              </Button>
            </div>
          </CardFooter>
        </Card>
      </div>
    </Layout>;
};
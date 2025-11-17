import React, { useState } from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody, CardFooter } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { CalendarIcon, ClockIcon, UserIcon, FileTextIcon, CheckCircleIcon } from 'lucide-react';
export const MeetingScheduler = () => {
  const [meetingDate, setMeetingDate] = useState('');
  const [meetingTime, setMeetingTime] = useState('');
  const [meetingNotes, setMeetingNotes] = useState('');
  const [adjustedScores, setAdjustedScores] = useState<{
    [key: number]: number | null;
  }>({});
  const [employeeSignature, setEmployeeSignature] = useState('');
  const [meetingScheduled, setMeetingScheduled] = useState(false);
  const appraisalData = {
    employeeName: 'Michael Johnson',
    employeeId: 'EMP-1234',
    period: 'Q4 2023',
    sections: [{
      id: 1,
      title: 'Goal Achievement',
      managerScore: 3,
      weight: 30
    }, {
      id: 2,
      title: 'Compliance Metrics',
      managerScore: 2,
      weight: 20
    }, {
      id: 3,
      title: 'Technical Competencies',
      managerScore: 2,
      weight: 25
    }, {
      id: 4,
      title: 'Behavioral Competencies',
      managerScore: 3,
      weight: 25
    }]
  };
  const agendaItems = ['Review overall performance ratings and discuss achievements', 'Address any concerns or questions about the appraisal', 'Discuss development opportunities and growth areas', 'Set goals for the next appraisal period', 'Review and acknowledge final appraisal results'];
  const handleScheduleMeeting = () => {
    if (!meetingDate || !meetingTime) {
      alert('Please select both date and time for the meeting');
      return;
    }
    setMeetingScheduled(true);
    alert(`Meeting scheduled for ${meetingDate} at ${meetingTime}. Employee has been notified.`);
  };
  const handleScoreAdjustment = (id: number, score: number) => {
    setAdjustedScores({
      ...adjustedScores,
      [id]: score
    });
  };
  const handleFinalizeAppraisal = () => {
    if (!employeeSignature.trim()) {
      alert('Employee signature is required to finalize the appraisal');
      return;
    }
    if (meetingNotes.trim().length === 0) {
      alert('Please enter meeting notes before finalizing');
      return;
    }
    alert('Appraisal finalized successfully. Notes have been saved and linked to the appraisal form.');
  };
  return <Layout title="Review Meeting Scheduler" subtitle="Schedule and conduct performance review meetings">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2 space-y-6">
          <Card>
            <CardHeader>
              <div className="flex justify-between items-center">
                <div>
                  <h2 className="text-lg font-medium text-gray-900">
                    Review Meeting: {appraisalData.employeeName}
                  </h2>
                  <p className="text-sm text-gray-500">
                    {appraisalData.employeeId} • {appraisalData.period}
                  </p>
                </div>
                {meetingScheduled ? <Badge variant="success">
                    <CheckCircleIcon className="h-3 w-3 mr-1" />
                    Meeting Scheduled
                  </Badge> : <Badge variant="warning">Pending Schedule</Badge>}
              </div>
            </CardHeader>
            <CardBody>
              <div className="mb-6">
                <h3 className="text-sm font-medium text-gray-700 mb-3">
                  Pre-populated Agenda
                </h3>
                <div className="bg-gray-50 p-4 rounded-md">
                  <ul className="space-y-2">
                    {agendaItems.map((item, index) => <li key={index} className="flex items-start">
                        <span className="flex-shrink-0 h-5 w-5 rounded-full bg-primary text-white flex items-center justify-center text-xs mr-3 mt-0.5">
                          {index + 1}
                        </span>
                        <span className="text-sm text-gray-700">{item}</span>
                      </li>)}
                  </ul>
                </div>
              </div>
              <div className="mb-6">
                <h3 className="text-sm font-medium text-gray-700 mb-3">
                  Current Performance Scores
                </h3>
                <div className="overflow-x-auto">
                  <table className="min-w-full divide-y divide-gray-200">
                    <thead className="bg-gray-50">
                      <tr>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                          Category
                        </th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                          Weight
                        </th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                          Manager Score
                        </th>
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">
                          Adjusted Score
                        </th>
                      </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                      {appraisalData.sections.map(section => <tr key={section.id}>
                          <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                            {section.title}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                            {section.weight}%
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                            {section.managerScore}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap">
                            <select value={adjustedScores[section.id] || ''} onChange={e => handleScoreAdjustment(section.id, parseInt(e.target.value))} className="text-sm border-gray-300 rounded-md focus:ring-primary focus:border-primary">
                              <option value="">No adjustment</option>
                              <option value="1">1 - Below Expectations</option>
                              <option value="2">2 - Meets Expectations</option>
                              <option value="3">
                                3 - Exceeds Expectations
                              </option>
                            </select>
                          </td>
                        </tr>)}
                    </tbody>
                  </table>
                </div>
                <p className="mt-2 text-xs text-gray-500">
                  Scores can be adjusted during the meeting if mutually agreed
                  upon
                </p>
              </div>
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Meeting Notes *
                </label>
                <textarea value={meetingNotes} onChange={e => setMeetingNotes(e.target.value)} rows={6} maxLength={1000} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Enter discussion points, agreements, and action items (max 1000 characters)" />
                <p className="mt-1 text-xs text-gray-500">
                  {meetingNotes.length}/1000 characters
                </p>
              </div>
              <div className="border-t border-gray-200 pt-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Employee Acknowledgment Signature
                </label>
                <input type="text" value={employeeSignature} onChange={e => setEmployeeSignature(e.target.value)} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Employee types full name to acknowledge final appraisal" />
                <p className="mt-1 text-xs text-gray-500">
                  Employee signature confirms acknowledgment of the final
                  appraisal results
                </p>
              </div>
            </CardBody>
            <CardFooter>
              <Button variant="primary" onClick={handleFinalizeAppraisal} fullWidth>
                Finalize Appraisal
              </Button>
            </CardFooter>
          </Card>
        </div>
        <div className="lg:col-span-1 space-y-6">
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">
                Schedule Meeting
              </h2>
            </CardHeader>
            <CardBody>
              <div className="space-y-4">
                <div className="bg-blue-50 p-4 rounded-md text-center">
                  <CalendarIcon className="h-12 w-12 text-primary mx-auto mb-2" />
                  <p className="text-sm text-gray-600">
                    Schedule a review meeting to discuss appraisal results
                  </p>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Meeting Date *
                  </label>
                  <input type="date" value={meetingDate} onChange={e => setMeetingDate(e.target.value)} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Meeting Time *
                  </label>
                  <input type="time" value={meetingTime} onChange={e => setMeetingTime(e.target.value)} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Duration
                  </label>
                  <select className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md">
                    <option>30 minutes</option>
                    <option>45 minutes</option>
                    <option>60 minutes</option>
                    <option>90 minutes</option>
                  </select>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">
                    Location/Meeting Link
                  </label>
                  <input type="text" className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Conference room or video call link" />
                </div>
              </div>
            </CardBody>
            <CardFooter>
              <Button variant="primary" onClick={handleScheduleMeeting} fullWidth>
                <CalendarIcon className="h-4 w-4 mr-2" />
                Schedule Meeting
              </Button>
            </CardFooter>
          </Card>
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">Reminder</h2>
            </CardHeader>
            <CardBody>
              <div className="bg-yellow-50 p-4 rounded-md">
                <p className="text-sm text-yellow-800">
                  System will remind you if the meeting is not scheduled within
                  5 business days of appraisal submission.
                </p>
              </div>
            </CardBody>
          </Card>
        </div>
      </div>
    </Layout>;
};
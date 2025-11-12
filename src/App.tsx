import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { EmployeeSelfAssessment } from './pages/EmployeeSelfAssessment';
import { ManagerReview } from './pages/ManagerReview';
import { MeetingScheduler } from './pages/MeetingScheduler';
import { HRMediation } from './pages/HRMediation';
import { AppealSubmission } from './pages/AppealSubmission';
import { HRDashboard } from './pages/HRDashboard';
export function App() {
  return <Router>
      <Routes>
        <Route path="/" element={<EmployeeSelfAssessment />} />
        <Route path="/manager-review" element={<ManagerReview />} />
        <Route path="/meeting-scheduler" element={<MeetingScheduler />} />
        <Route path="/hr-mediation" element={<HRMediation />} />
        <Route path="/appeal-submission" element={<AppealSubmission />} />
        <Route path="/hr-dashboard" element={<HRDashboard />} />
        <Route path="*" element={<EmployeeSelfAssessment />} />
      </Routes>
    </Router>;
}
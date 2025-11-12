import React, { useState } from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody, CardFooter } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { SaveIcon, SendIcon, AlertCircleIcon, LockIcon } from 'lucide-react';
export const EmployeeSelfAssessment = () => {
  const [isDraft, setIsDraft] = useState(true);
  const reviewCycleInfo = {
    cycle: 'H2 2023',
    period: 'Jul-Dec 2023',
    reviewWindow: 'Dec 1-15, 2023',
    daysRemaining: 8,
    goalsLocked: true
  };
  const assessmentSections = [{
    id: 1,
    title: 'Goal Achievement',
    description: 'Evaluate performance against functional and organizational goals',
    weight: 70,
    goalText: 'Increase quarterly sales revenue by 15% and complete digital transformation project',
    actualPerformance: '',
    score: null
  }, {
    id: 2,
    title: 'Compliance & Learning',
    description: 'Adherence to regulatory requirements and completion of mandatory training',
    weight: 20,
    goalText: 'Complete all mandatory compliance training and achieve 100% on assessments',
    actualPerformance: '',
    score: null
  }, {
    id: 3,
    title: 'Core Competencies',
    description: 'Behavioral competencies and skills development',
    weight: 10,
    goalText: 'Demonstrate leadership, teamwork, and effective communication skills',
    actualPerformance: '',
    score: null
  }];
  const [sections, setSections] = useState(assessmentSections);
  const [signature, setSignature] = useState('');
  const [errors, setErrors] = useState<string[]>([]);
  const handleScoreChange = (id: number, score: number) => {
    setSections(sections.map(section => section.id === id ? {
      ...section,
      score
    } : section));
  };
  const handleActualPerformanceChange = (id: number, value: string) => {
    if (value.length <= 500) {
      setSections(sections.map(section => section.id === id ? {
        ...section,
        actualPerformance: value
      } : section));
    }
  };
  const calculateWeightedScore = () => {
    const total = sections.reduce((acc, section) => {
      if (section.score !== null) {
        return acc + section.score * section.weight / 100;
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
  const validateForm = () => {
    const newErrors: string[] = [];
    sections.forEach(section => {
      if (!section.actualPerformance.trim()) {
        newErrors.push(`Actual performance for "${section.title}" is required`);
      }
      if (section.score === null) {
        newErrors.push(`Score for "${section.title}" is required`);
      }
    });
    if (!signature.trim()) {
      newErrors.push('Digital signature is required');
    }
    setErrors(newErrors);
    return newErrors.length === 0;
  };
  const handleSaveDraft = () => {
    setIsDraft(true);
    alert('Self-assessment saved as draft');
  };
  const handleSubmit = () => {
    if (validateForm()) {
      setIsDraft(false);
      alert('Self-assessment submitted successfully. Your manager has been notified.');
    }
  };
  const weightedScore = parseFloat(calculateWeightedScore());
  const performanceStandard = getPerformanceStandard(weightedScore);
  return <Layout title="Employee Self-Assessment" subtitle="Complete your performance self-assessment for the current appraisal period">
      <div className="space-y-6">
        <Card>
          <CardHeader>
            <div className="flex justify-between items-center">
              <div>
                <h2 className="text-lg font-medium text-gray-900">
                  Review Cycle: {reviewCycleInfo.cycle}
                </h2>
                <p className="mt-1 text-sm text-gray-500">
                  Period: {reviewCycleInfo.period} • Review Window:{' '}
                  {reviewCycleInfo.reviewWindow}
                </p>
              </div>
              <div className="flex items-center space-x-2">
                <Badge variant={isDraft ? 'warning' : 'success'}>
                  {isDraft ? 'Draft' : 'Submitted'}
                </Badge>
                <Badge variant="default">
                  {reviewCycleInfo.daysRemaining} days remaining
                </Badge>
              </div>
            </div>
          </CardHeader>
          <CardBody>
            <div className="mb-6 bg-blue-50 border border-blue-200 rounded-md p-4">
              <div className="flex">
                <AlertCircleIcon className="h-5 w-5 text-blue-600 mr-2" />
                <div>
                  <h3 className="text-sm font-medium text-blue-900">
                    Instructions
                  </h3>
                  <p className="mt-1 text-sm text-blue-700">
                    Review each pre-populated goal and competency. Enter your
                    actual performance (max 500 characters) and select a score
                    from 1-3. You can save as draft and submit later with your
                    digital signature.
                  </p>
                  <p className="mt-2 text-xs text-blue-600">
                    Score Legend: 1 = Below Expectations, 2 = Meets
                    Expectations, 3 = Exceeds Expectations
                  </p>
                </div>
              </div>
            </div>
            {reviewCycleInfo.goalsLocked && <div className="mb-6 bg-yellow-50 border border-yellow-200 rounded-md p-4">
                <div className="flex items-center">
                  <LockIcon className="h-5 w-5 text-yellow-600 mr-2" />
                  <div>
                    <h3 className="text-sm font-medium text-yellow-900">
                      Goals Locked for Review Period
                    </h3>
                    <p className="mt-1 text-sm text-yellow-700">
                      Goals for this review period are locked and cannot be
                      modified. All assessments are based on the goals set at
                      the beginning of {reviewCycleInfo.period}.
                    </p>
                  </div>
                </div>
              </div>}
            {errors.length > 0 && <div className="mb-6 bg-red-50 border border-red-200 rounded-md p-4">
                <h3 className="text-sm font-medium text-red-900 mb-2">
                  Please fix the following errors:
                </h3>
                <ul className="list-disc list-inside text-sm text-red-700">
                  {errors.map((error, index) => <li key={index}>{error}</li>)}
                </ul>
              </div>}
            <div className="space-y-8">
              {sections.map(section => <div key={section.id} className="border border-gray-200 rounded-lg p-6">
                  <div className="flex justify-between items-start mb-4">
                    <div>
                      <h3 className="text-base font-medium text-gray-900">
                        {section.title}
                      </h3>
                      <p className="text-sm text-gray-500">
                        {section.description}
                      </p>
                    </div>
                    <Badge variant="default">Weight: {section.weight}%</Badge>
                  </div>
                  <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Pre-populated Goal/Competency (Locked)
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
                      Actual Performance *
                    </label>
                    <textarea value={section.actualPerformance} onChange={e => handleActualPerformanceChange(section.id, e.target.value)} rows={4} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Describe your actual performance and achievements (max 500 characters)" maxLength={500} />
                    <p className="mt-1 text-xs text-gray-500">
                      {section.actualPerformance.length}/500 characters
                    </p>
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Self-Assessment Score *
                    </label>
                    <div className="flex space-x-4">
                      {[1, 2, 3].map(score => <button key={score} onClick={() => handleScoreChange(section.id, score)} className={`flex-1 py-3 px-4 border rounded-md text-sm font-medium transition-colors ${section.score === score ? 'bg-primary text-white border-primary' : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50'}`}>
                          {score === 1 && '1 - Below Expectations'}
                          {score === 2 && '2 - Meets Expectations'}
                          {score === 3 && '3 - Exceeds Expectations'}
                        </button>)}
                    </div>
                  </div>
                </div>)}
            </div>
            {sections.every(s => s.score !== null) && <div className="mt-8 border-t border-gray-200 pt-6">
                <h3 className="text-base font-medium text-gray-900 mb-4">
                  Performance Summary
                </h3>
                <div className="bg-gradient-to-r from-blue-50 to-indigo-50 p-6 rounded-lg">
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <div>
                      <p className="text-sm text-gray-600 mb-2">
                        Weighted Score
                      </p>
                      <p className="text-4xl font-bold text-primary">
                        {weightedScore}%
                      </p>
                    </div>
                    <div>
                      <p className="text-sm text-gray-600 mb-2">
                        Performance Standard
                      </p>
                      <Badge variant={performanceStandard.color as any} className="text-base px-4 py-2">
                        {performanceStandard.label}
                      </Badge>
                    </div>
                  </div>
                  <div className="mt-4 pt-4 border-t border-gray-200">
                    <p className="text-xs text-gray-600">
                      Scoring: Below 50% = Unacceptable | 50-59% = Average |
                      60-79% = Satisfactory | 80%+ = Exceptional
                    </p>
                  </div>
                </div>
              </div>}
            <div className="mt-8 border-t border-gray-200 pt-6">
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Digital Signature *
              </label>
              <input type="text" value={signature} onChange={e => setSignature(e.target.value)} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Type your full name to sign" />
              <p className="mt-1 text-xs text-gray-500">
                By signing, you confirm that the information provided is
                accurate and complete.
              </p>
            </div>
          </CardBody>
          <CardFooter>
            <div className="flex justify-between w-full">
              <Button variant="secondary" onClick={handleSaveDraft}>
                <SaveIcon className="h-4 w-4 mr-2" />
                Save as Draft
              </Button>
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
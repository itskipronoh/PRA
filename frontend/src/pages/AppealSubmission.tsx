import React, { useState } from 'react';
import { Layout } from '../components/Layout';
import { Card, CardHeader, CardBody, CardFooter } from '../components/ui/Card';
import { Button } from '../components/ui/Button';
import { Badge } from '../components/ui/Badge';
import { UploadIcon, FileTextIcon, AlertCircleIcon, CheckCircleIcon } from 'lucide-react';
export const AppealSubmission = () => {
  const [reason, setReason] = useState('');
  const [requestedOutcome, setRequestedOutcome] = useState('');
  const [uploadedFiles, setUploadedFiles] = useState<File[]>([]);
  const [appealStatus, setAppealStatus] = useState<string | null>(null);
  const appraisalInfo = {
    period: 'Q4 2023',
    submittedDate: '2023-12-15',
    overallRating: 'Meets Expectations',
    weightedAverage: 2.35,
    daysRemaining: 3
  };
  const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;
    if (files) {
      const fileArray = Array.from(files);
      const validFiles = fileArray.filter(file => file.size <= 5 * 1024 * 1024); // 5MB limit
      if (validFiles.length !== fileArray.length) {
        alert('Some files exceed the 5MB limit and were not uploaded');
      }
      setUploadedFiles([...uploadedFiles, ...validFiles]);
    }
  };
  const removeFile = (index: number) => {
    setUploadedFiles(uploadedFiles.filter((_, i) => i !== index));
  };
  const handleSubmitAppeal = () => {
    if (!reason.trim()) {
      alert('Please provide a reason for your appeal');
      return;
    }
    if (!requestedOutcome.trim()) {
      alert('Please specify your requested outcome');
      return;
    }
    setAppealStatus('Pending Review');
    alert('Appeal submitted successfully. Your Senior Line Manager and HR have been notified.');
  };
  return <Layout title="Appeal Submission" subtitle="Submit an appeal if you disagree with your appraisal results">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-2">
          <Card>
            <CardHeader>
              <div className="flex justify-between items-center">
                <div>
                  <h2 className="text-lg font-medium text-gray-900">
                    Appraisal Appeal Form
                  </h2>
                  <p className="text-sm text-gray-500">
                    Period: {appraisalInfo.period}
                  </p>
                </div>
                {appealStatus ? <Badge variant="primary">{appealStatus}</Badge> : <Badge variant="warning">
                    {appraisalInfo.daysRemaining} days remaining
                  </Badge>}
              </div>
            </CardHeader>
            <CardBody>
              <div className="mb-6 bg-yellow-50 border border-yellow-200 rounded-md p-4">
                <div className="flex">
                  <AlertCircleIcon className="h-5 w-5 text-yellow-600 mr-2" />
                  <div>
                    <h3 className="text-sm font-medium text-yellow-900">
                      Important Notice
                    </h3>
                    <p className="mt-1 text-sm text-yellow-700">
                      You have {appraisalInfo.daysRemaining} days remaining to
                      submit an appeal. Appeals must be submitted within 5 days
                      of receiving your appraisal results.
                    </p>
                  </div>
                </div>
              </div>
              <div className="mb-6">
                <h3 className="text-sm font-medium text-gray-700 mb-3">
                  Your Appraisal Summary
                </h3>
                <div className="bg-gray-50 p-4 rounded-md">
                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <p className="text-xs text-gray-500">Overall Rating</p>
                      <p className="text-sm font-medium text-gray-900">
                        {appraisalInfo.overallRating}
                      </p>
                    </div>
                    <div>
                      <p className="text-xs text-gray-500">Weighted Average</p>
                      <p className="text-sm font-medium text-gray-900">
                        {appraisalInfo.weightedAverage}
                      </p>
                    </div>
                    <div>
                      <p className="text-xs text-gray-500">Submitted Date</p>
                      <p className="text-sm font-medium text-gray-900">
                        {appraisalInfo.submittedDate}
                      </p>
                    </div>
                    <div>
                      <p className="text-xs text-gray-500">Appeal Deadline</p>
                      <p className="text-sm font-medium text-red-600">
                        2023-12-20
                      </p>
                    </div>
                  </div>
                </div>
              </div>
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Reason for Appeal *
                </label>
                <textarea value={reason} onChange={e => setReason(e.target.value)} rows={6} maxLength={1000} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Explain why you believe your appraisal rating should be reviewed (max 1000 characters)" />
                <p className="mt-1 text-xs text-gray-500">
                  {reason.length}/1000 characters
                </p>
              </div>
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Supporting Evidence
                </label>
                <div className="mt-1 flex justify-center px-6 pt-5 pb-6 border-2 border-gray-300 border-dashed rounded-md">
                  <div className="space-y-1 text-center">
                    <UploadIcon className="mx-auto h-12 w-12 text-gray-400" />
                    <div className="flex text-sm text-gray-600">
                      <label className="relative cursor-pointer bg-white rounded-md font-medium text-primary hover:text-primary-dark">
                        <span>Upload files</span>
                        <input type="file" className="sr-only" multiple onChange={handleFileUpload} accept=".pdf,.doc,.docx,.jpg,.jpeg,.png" />
                      </label>
                      <p className="pl-1">or drag and drop</p>
                    </div>
                    <p className="text-xs text-gray-500">
                      PDF, DOC, DOCX, JPG, PNG up to 5MB each
                    </p>
                  </div>
                </div>
                {uploadedFiles.length > 0 && <div className="mt-4 space-y-2">
                    {uploadedFiles.map((file, index) => <div key={index} className="flex items-center justify-between bg-gray-50 p-3 rounded-md">
                        <div className="flex items-center">
                          <FileTextIcon className="h-5 w-5 text-gray-400 mr-2" />
                          <span className="text-sm text-gray-700">
                            {file.name}
                          </span>
                          <span className="ml-2 text-xs text-gray-500">
                            ({(file.size / 1024).toFixed(2)} KB)
                          </span>
                        </div>
                        <button onClick={() => removeFile(index)} className="text-red-600 hover:text-red-800 text-sm">
                          Remove
                        </button>
                      </div>)}
                  </div>}
              </div>
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Requested Outcome *
                </label>
                <textarea value={requestedOutcome} onChange={e => setRequestedOutcome(e.target.value)} rows={4} className="shadow-sm focus:ring-primary focus:border-primary block w-full sm:text-sm border-gray-300 rounded-md" placeholder="Specify what outcome you are seeking (e.g., rating adjustment, score revision)" />
              </div>
            </CardBody>
            <CardFooter>
              <Button variant="primary" onClick={handleSubmitAppeal} fullWidth disabled={appealStatus !== null}>
                Submit Appeal
              </Button>
            </CardFooter>
          </Card>
        </div>
        <div className="lg:col-span-1 space-y-6">
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">
                Appeal Process
              </h2>
            </CardHeader>
            <CardBody>
              <div className="space-y-4">
                <div className="flex items-start">
                  <div className="flex-shrink-0 h-8 w-8 rounded-full bg-primary text-white flex items-center justify-center text-sm font-medium">
                    1
                  </div>
                  <div className="ml-3">
                    <p className="text-sm font-medium text-gray-900">
                      Submit Appeal
                    </p>
                    <p className="text-xs text-gray-500">
                      Complete and submit the appeal form
                    </p>
                  </div>
                </div>
                <div className="flex items-start">
                  <div className="flex-shrink-0 h-8 w-8 rounded-full bg-gray-300 text-white flex items-center justify-center text-sm font-medium">
                    2
                  </div>
                  <div className="ml-3">
                    <p className="text-sm font-medium text-gray-900">
                      Senior Manager Review
                    </p>
                    <p className="text-xs text-gray-500">
                      Your appeal is reviewed by your Senior Line Manager
                    </p>
                  </div>
                </div>
                <div className="flex items-start">
                  <div className="flex-shrink-0 h-8 w-8 rounded-full bg-gray-300 text-white flex items-center justify-center text-sm font-medium">
                    3
                  </div>
                  <div className="ml-3">
                    <p className="text-sm font-medium text-gray-900">
                      HR Mediation (if needed)
                    </p>
                    <p className="text-xs text-gray-500">
                      Escalated to HR if not resolved
                    </p>
                  </div>
                </div>
                <div className="flex items-start">
                  <div className="flex-shrink-0 h-8 w-8 rounded-full bg-gray-300 text-white flex items-center justify-center text-sm font-medium">
                    4
                  </div>
                  <div className="ml-3">
                    <p className="text-sm font-medium text-gray-900">
                      Final Decision
                    </p>
                    <p className="text-xs text-gray-500">
                      You will be notified of the outcome
                    </p>
                  </div>
                </div>
              </div>
            </CardBody>
          </Card>
          {appealStatus && <Card>
              <CardHeader>
                <h2 className="text-lg font-medium text-gray-900">
                  Track Appeal Status
                </h2>
              </CardHeader>
              <CardBody>
                <div className="space-y-3">
                  <div className="flex items-center justify-between">
                    <span className="text-sm text-gray-700">
                      Current Status
                    </span>
                    <Badge variant="primary">{appealStatus}</Badge>
                  </div>
                  <div className="flex items-center justify-between">
                    <span className="text-sm text-gray-700">
                      Submitted Date
                    </span>
                    <span className="text-sm text-gray-900">2023-12-16</span>
                  </div>
                  <div className="flex items-center justify-between">
                    <span className="text-sm text-gray-700">
                      Expected Response
                    </span>
                    <span className="text-sm text-gray-900">
                      Within 10 days
                    </span>
                  </div>
                </div>
              </CardBody>
            </Card>}
          <Card>
            <CardHeader>
              <h2 className="text-lg font-medium text-gray-900">Need Help?</h2>
            </CardHeader>
            <CardBody>
              <p className="text-sm text-gray-600 mb-4">
                Contact HR if you have questions about the appeal process
              </p>
              <Button variant="outline" fullWidth>
                Contact HR Support
              </Button>
            </CardBody>
          </Card>
        </div>
      </div>
    </Layout>;
};
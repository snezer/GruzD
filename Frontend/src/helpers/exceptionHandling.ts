export type ErrorCategory = 'network' | 'service' | 'client';

export type ErrorSeverity = 'low' | 'medium' | 'critical';

export interface IErrorInfo {
  message: string | null;
  category: ErrorCategory;
  severity: ErrorSeverity;
  correlationId: string | null;
  timestamp: Date | null;
  remedy: string | null;
  contact: string | null;
}

export class SecurityError extends Error {
  constructor(public code: string, message?: string, public details?: any) {
    super(message);
    this.name = 'SecurityError';
    Object.setPrototypeOf(this, SecurityError.prototype);
  }
}

export class GenericError extends Error {
  constructor(message?: string, public details?: any) {
    super(message);
    this.name = 'GenericError';
    Object.setPrototypeOf(this, GenericError.prototype);
  }
}

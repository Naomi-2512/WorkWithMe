export interface User {
  UserId: string,
  Fullname: string,
  Email: string,
  Mobile: string,
  Country: string,
  City: string,
  Password: string,
  ProfilePicture: string,
  Role: string,
  CreatedAt: Date,
  Jobs?: Job[],
  Payments?: Payment[],
  Approvals?: Approval[]
};

export interface Job {
  JobId: string,
  UserId: string,
  Title: string,
  CompanyName: string,
  JobPositions: string[],
  Category: string,
  Country: string,
  City: string,
  Description: string,
  CreatedAt: Date,
  IsActivated: boolean,
  Positions: Position[],
  Approvals: Approval[],
  Payments: Payment[]
}

export interface Position {
  PositionId: string,
  JobId: string,
  Title: string,
  Price: number,
  RequiredUsers: number,
  TimePeriod: string,
  Job?: Job
}

export interface Payment {
  PaymentId: string,
  JobId: string,
  UserId: string,
  IsVerified: boolean,
  Price: number,
  AdditionalPrice: number,
  User: User,
  Job: Job
}

export interface Approval {
  ApprovalId: string,
  JobId: string,
  OwnerId: string,
  ApplierId: string,
  IsApproved: boolean,
  IsRejected: string,
  PositionApplied: string,
  Owner?: User,
  Applier?: User,
  Job?: Job
}

export interface LoginDetails {
  email: string,
  password: string
}
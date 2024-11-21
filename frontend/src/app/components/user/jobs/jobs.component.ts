import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-jobs',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink,RouterModule],
  templateUrl: './jobs.component.html',
  styleUrl: './jobs.component.css'
})
export class JobsComponent {
  jobs = [
    {
      title: 'Frontend Developer',
      description: 'Develop and maintain user-facing features.',
      amount: 50000,
      dateCreated: '2024-11-18'
    },
    {
      title: 'Backend Developer',
      description: 'Design and manage APIs and server-side logic.',
      amount: 60000,
      dateCreated: '2024-11-17'
    },
    {
      title: 'Project Manager',
      description: 'Coordinate team efforts and project timelines.',
      amount: 70000,
      dateCreated: '2024-11-16'
    },
    {
      title: 'Graphic Designer',
      description: 'Create visual content for branding and marketing.',
      amount: 40000,
      dateCreated: '2024-11-15'
    }
  ];

}

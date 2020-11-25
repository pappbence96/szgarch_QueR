import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-site-title',
  templateUrl: './site-title.component.html',
  styleUrls: ['./site-title.component.css']
})
export class SiteTitleComponent implements OnInit {
  @Input() title: string;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  goBack(): void {
    this.router.navigate(['..']);
  }
}

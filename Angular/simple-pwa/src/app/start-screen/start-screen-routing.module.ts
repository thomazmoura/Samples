import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartScreenComponent } from './start-screen.component';

const routes: Routes = [
  {
    path: '',
    component: StartScreenComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StartScreenRoutingModule { }

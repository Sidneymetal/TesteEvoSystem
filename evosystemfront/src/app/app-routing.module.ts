import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartamentoComponent } from './departamento/departamento.component';
import { FuncionarioComponent } from './funcionario/funcionario.component';

const routes: Routes = [
  { path: 'departamentos', component: DepartamentoComponent },
  { path: 'funcionarios/:departamentoId', component: FuncionarioComponent },
  { path: '', component: DepartamentoComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

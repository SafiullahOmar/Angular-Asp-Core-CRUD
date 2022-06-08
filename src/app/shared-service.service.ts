import { Injectable } from '@angular/core';
import { HttpClient} from  '@angular/common/http';
import{Observable, observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  readonly apiURL="https://localhost:44362/api";
  readonly photoURL="https://localhost:44362/photo";

  constructor(private http:HttpClient) { }

  getDepartmentList():Observable<any[]>{
    return this.http.get<any>(this.apiURL+'/department');
  }

  addDepartment(dept:any){
    return this.http.post(this.apiURL+'/department',dept);
  }

  updateDepartment(dept:any){
    return this.http.put(this.apiURL+'/Department',dept);
  }

  deleteDepartment(dept:any){
    return this.http.delete(this.apiURL+'/Department/'+dept);
  }


  //Employee APIs

  getEmployeeList():Observable<any[]>{
    return this.http.get<any>(this.apiURL+'/Employee');
  }

  addEmployee(dept:any){
    return this.http.post(this.apiURL+'Employee',dept);
  }

  updateEmployee(dept:any){
    return this.http.put(this.apiURL+'/Employee',dept);
  }

  deleteEmployee(dept:any){
    return this.http.delete(this.apiURL+'/Employee/'+dept);
  }

  SavePhoto(val:any){
    return this.http.post(this.apiURL+'/Emmployee/SaveFile',val);
  }

  getAllDeptNames():Observable<any[]>{
    return this.http.get<any[]>(this.apiURL+'/Empoyee/GetAllDpt')
  }

}

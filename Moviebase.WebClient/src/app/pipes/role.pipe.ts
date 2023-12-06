import { Pipe, PipeTransform } from "@angular/core";
import { Logged } from "../models/logged";

@Pipe({
    name: 'role'
})
export class RolePipe implements PipeTransform {
  transform (input: Logged | null, role: string): boolean {
      return input?.user?.roles?.includes(role) ?? false;
  }
}
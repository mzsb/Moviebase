import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'join'
})
export class JoinPipe implements PipeTransform {
  transform (input: string[], separator: string): string {
      return input.join(separator);
  }
}
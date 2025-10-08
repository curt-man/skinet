import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, identity } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { environment } from '../../../environments/environment';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);
  let randomDelay = Math.random()*1000

  busyService.busy();
  return next(req).pipe(
    (environment.production ? identity : delay(200)),
    finalize(()=>busyService.idle())
  )
};

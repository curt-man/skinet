import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);
  let randomDelay = Math.random()*1000

  busyService.busy();
  return next(req).pipe(
    delay(randomDelay),
    finalize(()=>busyService.idle())
  )
};

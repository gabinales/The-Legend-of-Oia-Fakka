INCLUDE ../globals.ink

EXTERNAL MudaHora(id)

Dormir até: #speaker:  
 +{HoraDoDia != 0} Manhã
 ~HoraDoDia = 0
 ~MudaHora(0)
 ->END

 +{HoraDoDia != 1} Tarde
  ~HoraDoDia = 1
  ~MudaHora(1)
 ->END

 +{HoraDoDia != 2} Noite
   ~HoraDoDia = 2
   ~MudaHora(2)
  ->END

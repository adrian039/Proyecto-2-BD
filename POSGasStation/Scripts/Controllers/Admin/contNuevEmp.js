angular.module("mainModule").controller("contNuevEmp", ["$scope","$http","directionService",'storeService','$location','userService',

  function($scope,$http,directionService,storeService,$location,userService) {
  
    var rol;


      $scope.getRols=function(){
        var url = 'http://'+getIp()+':58706/api/Roles/'+userService.getCompany();
        $http.get(url).then(function(msg){
          $scope.rolList= msg.data;
        });
      }
      $scope.setRol=function(pRol){
          rol = pRol;
      }

      

      $scope.createEmployee = function (username, password, conPassword, name, surname, sSurname, id, dir, date, email,admin,sucAd) {
        if(admin){rol=1;} 
        if(!sucAd){storeService.setID(userService.getSucursal());}
        if (password == conPassword) {
                var url = 'http://'+getIp()+':58706/api/Empleados';
                var sendData = {
                  "idEmpleado": parseInt(id),
                  "Email": email,
                  "Username": username,
                  "Password": password,
                  "Nombre": name,
                  "pApellido": surname,
                  "sApellido": sSurname,
                  "Nacimiento": date,
                  "Direccion": dir,
                  "Estado": 1,
                  "idRol": rol,
                  "idSucursal": storeService.getID()
                }
                console.log("employee: "+sendData);
                $scope.postHttp(url,sendData,(data)=>{
                  alert('created Admin');
                  storeService.cleanStore();
                  $location.path('/Admin/gsucursales');
                })
              }
              else {
                alert("Error(03): Passwords not match");
              }
        
          };
      }]);
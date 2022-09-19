app.controller('StudentQueryMasterFacultyCtrl', function ($scope, $http, $localStorage, $window, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage StudentQueryMaster-Faculty";
 
    $scope.FacRemarks = {};
    $scope.View = false;
    $scope.HideBtn = true;
  
    $scope.giveRemarks = function () {
        $scope.View = true;
        $scope.HideBtn = false;
    }


    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.getStudentQueryList = function () {
        var data = new Object();

        $http({
            method: 'Get',
            url: 'api/StudentQueryMasterFaculty/StudentQueryMasterGetByFaculty',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.StudentQueryTableParams = new NgTableParams
                        ({},{ dataset: response.obj });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
   
    $scope.getStudentQueryListById = function () {
        
        var Id = "";
        $http({
            method: 'POST',
            url: 'api/StudentQueryMasterFaculty/StudentQueryMasterGetById',
            data: { Id: $localStorage.StudentQueryIdForGettingDetails},

            headers: { "Content-Type": 'application/json' }
        }).success(function (response) {
           $scope.StudentQueryDesk = response.obj[0];         
            $scope.FacRemarks.Id = $localStorage.StudentQueryIdForGettingDetails;
            $scope.FacRemarks.EmailId = $scope.StudentQueryDesk.EmailId;
            $scope.FacRemarks.FirstName = $scope.StudentQueryDesk.FirstName;          
            $scope.FacRemarks.LastName = $scope.StudentQueryDesk.LastName;
            $scope.FacRemarks.FacultyRemarks = $scope.StudentQueryDesk.FacultyRemarks;
             
        })
        .error(function (res) {
            alert(res);
        });
    };
   $scope.getStudentQueryFacultyRemarksById = function () {
        var data = {};
        data.Id = $localStorage.StudentQueryIdForGettingDetails;
        $http({
            method: 'POST',
            url: 'api/StudentQueryMasterFaculty/StudentQueryMasterGetFacultyRemarksById',
            data: data,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.StudentQueryFacRemarksTableParams = new NgTableParams
                        ({}, { dataset: response.obj });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addFacultyRemarks = function () {
        
        if ($scope.FacRemarks.FacultyRemarks === null || $scope.FacRemarks.FacultyRemarks === undefined || $scope.FacRemarks.FacultyRemarks === "" )
           
            {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Type Faculty Remarks!")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/StudentQueryMasterFaculty/StudentQueryMasterFacultyRemarksAdd',
                data: $scope.FacRemarks,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(respons.obj);
                        $scope.offSpinner();
                    }
                    else {
                        $scope.offSpinner();
                        alert(response.obj);                                             
                        $scope.getStudentQueryFacultyRemarksById();
                        //$scope.Faculty = {};
                        $scope.View = false;
                        $scope.HideBtn = true;
                       
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }

   
    $scope.showFormFlag = true;
    $scope.modifyFacultyRemarksData = function (data) {
        $scope.showFormFlag = true;
        $scope.StuQueryFacRemarks = data;

    }

   
    
    $scope.newStudentQueryFacultyRemarksAdd = function (StudentQueryId) {
        $localStorage.StudentQueryIdForGettingDetails = StudentQueryId;
        $state.go('StudentQueryFacultyRemarksAdd');
    };

    $scope.backToList = function () {
        $state.go('StudentQueryMasterFaculty');
    };
});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}
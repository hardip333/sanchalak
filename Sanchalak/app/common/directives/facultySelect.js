app.directive("facultySelect", function () {
    return {
        templateUrl: "UI/components/facultySelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstFacultyGetForDropDownList = [];
            $scope.defaultFacultyList = [];
            var facObj = {
                Id: 0,
                FacultyName: "Select Faculty"
            }
            $scope.defaultFacultyList.push(facObj);

            $scope.MstFacultyGetForDropDown = function () {

                $http({
                    method: 'POST',
                    url: 'api/mstfaculty/MstFacultyGetForDropDown',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstFacultyGetForDropDownList = $scope.defaultFacultyList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                     
                    });
            }
            $scope.MstFacultyGetForDropDown();


        }],
        link: function (e, t, a) {
        }
    }
})
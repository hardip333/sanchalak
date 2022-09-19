app.directive("programmeSelect", function () {
    return {
        templateUrl: "UI/components/programmeSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammeGetByFacultyIdList = [];
            $scope.defaultProgrammeList = [];
            var programmeObj = {
                Id: 0,
                ProgrammeName: "Select Programme"
            }
            $scope.defaultProgrammeList.push(programmeObj);

            $scope.MstProgrammeGetByFacultyId = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/mstprogramme/mstprogrammegetbyfacultyid',
                    data: { FacultyId: data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.MstProgrammeGetByFacultyIdList = $scope.defaultProgrammeList.concat($scope.tempList);

                         /*   $rootScope.$broadcast("countryList");*/

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
    /*        $scope.MstProgrammeGetByFacultyId();*/


        }],
        link: function (e, t, a) {
        }
    }
})
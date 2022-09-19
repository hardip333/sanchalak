app.directive("proinsparttermwithproparttermSelect", function () {
    return {
        templateUrl: "UI/components/proinsparttermwithproparttermSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.IncProgrammeInstancePartTermGetByProgrammePartTermIdList = [];
            $scope.defaultProinsparttermwithparttermList = [];
            var insObj = {
                Id : 0,
                InstancePartTermName: "Select Programme Instance PartTerm"
            }
            $scope.defaultProinsparttermwithparttermList.push(insObj);

            $scope.getIncProgrammeInstancePartTermGetByProgrammePartTermId = function (data) {

                $rootScope.showLoading = true;
                if (data.ProgrammeId !== null && data.ExamMasterId !== null && data.FacultyExamMapId  !== null) {

                    $http({
                        method: 'POST',
                        url: 'api/MstProgrammeInstancePartTerm/IncProgrammeInstancePartTermGetByProgrammePartTermId',
                        data: data,
                        headers: { "Content-Type": 'application/json' }
                    })
                        .success(function (response) {

                            $rootScope.showLoading = false;

                            if (response.response_code != "200") {

                                alert(response.obj);
                            }
                            else {

                                $scope.tempList = response.obj;

                                $scope.IncProgrammeInstancePartTermGetByProgrammePartTermIdList = $scope.defaultProinsparttermwithparttermList.concat($scope.tempList);

                            }
                        })
                        .error(function (res) {

                            alert(res.obj);
                        });
                }
            }
      /*      $scope.getIncProgrammeInstancePartTermGetByProgrammePartTermId();*/


        }],
        link: function (e, t, a) {
        }
    }
})
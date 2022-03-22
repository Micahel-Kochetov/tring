ÿ»
wC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\SceneParentLoading\SceneParentAutomaticLoader.cs
	namespace		 	
Zenject		
 
.		 
Internal		 
{

 
[ 
InitializeOnLoad 
] 
public 

static 
class &
SceneParentAutomaticLoader 2
{ 
static &
SceneParentAutomaticLoader )
() *
)* +
{ 	
EditorApplication 
.  
playModeStateChanged 2
+=3 5"
OnPlayModeStateChanged6 L
;L M
} 	
static 
void "
OnPlayModeStateChanged *
(* +
PlayModeStateChange+ >
state? D
)D E
{ 	
if 
( 
state 
== 
PlayModeStateChange ,
., -
ExitingEditMode- <
)< =
{ 
try 
{ =
1ValidateMultiSceneSetupAndLoadDefaultSceneParents E
(E F
)F G
;G H
} 
catch 
( 
	Exception  
e! "
)" #
{ 
EditorApplication %
.% &
	isPlaying& /
=0 1
false2 7
;7 8
throw 
new 
ZenjectException .
(. /
$str b
,b c
ed e
)e f
;f g
}   
}!! 
else"" 
if"" 
("" 
state"" 
=="" 
PlayModeStateChange"" 1
.""1 2
EnteredEditMode""2 A
)""A B
{## 
}'' 
}(( 	
public** 
static** 
void** =
1ValidateMultiSceneSetupAndLoadDefaultSceneParents** L
(**L M
)**M N
{++ 	
var,, 
defaultContractsMap,, #
=,,$ %#
LoadDefaultContractsMap,,& =
(,,= >
),,> ?
;,,? @
var11 

sceneInfos11 
=11 &
GetLoadedZenjectSceneInfos11 7
(117 8
)118 9
;119 :
var22 
contractMap22 
=22 '
GetCurrentSceneContractsMap22 9
(229 :

sceneInfos22: D
)22D E
;22E F
foreach44 
(44 
var44 
	sceneInfo44 "
in44# %

sceneInfos44& 0
)440 1
{55 
ProcessScene66 
(66 
	sceneInfo66 &
,66& '
contractMap66( 3
,663 4
defaultContractsMap665 H
)66H I
;66I J
}77 
}88 	
static:: 

Dictionary:: 
<:: 
string::  
,::  !
LoadedSceneInfo::" 1
>::1 2'
GetCurrentSceneContractsMap::3 N
(::N O
List;; 
<;; 
LoadedSceneInfo;;  
>;;  !

sceneInfos;;" ,
);;, -
{<< 	
var== 
contractMap== 
=== 
new== !

Dictionary==" ,
<==, -
string==- 3
,==3 4
LoadedSceneInfo==5 D
>==D E
(==E F
)==F G
;==G H
foreach?? 
(?? 
var?? 
info?? 
in??  

sceneInfos??! +
)??+ ,
{@@ 
AddToContractMapAA  
(AA  !
contractMapAA! ,
,AA, -
infoAA. 2
)AA2 3
;AA3 4
}BB 
returnDD 
contractMapDD 
;DD 
}EE 	
staticGG 
voidGG 
ProcessSceneGG  
(GG  !
LoadedSceneInfoHH 
	sceneInfoHH %
,HH% &

DictionaryII 
<II 
stringII 
,II 
LoadedSceneInfoII .
>II. /
contractMapII0 ;
,II; <

DictionaryJJ 
<JJ 
stringJJ 
,JJ 
stringJJ %
>JJ% &
defaultContractsMapJJ' :
)JJ: ;
{KK 	
ifLL 
(LL 
	sceneInfoLL 
.LL 
SceneContextLL &
!=LL' )
nullLL* .
)LL. /
{MM 
AssertNN 
.NN 
IsNullNN 
(NN 
	sceneInfoNN '
.NN' (
DecoratorContextNN( 8
)NN8 9
;NN9 :
ProcessSceneParentsOO #
(OO# $
	sceneInfoOO$ -
,OO- .
contractMapOO/ :
,OO: ;
defaultContractsMapOO< O
)OOO P
;OOP Q
}PP 
elseQQ 
{RR 
AssertSS 
.SS 
	IsNotNullSS  
(SS  !
	sceneInfoSS! *
.SS* +
DecoratorContextSS+ ;
)SS; <
;SS< ="
ProcessSceneDecoratorsTT &
(TT& '
	sceneInfoTT' 0
,TT0 1
contractMapTT2 =
,TT= >
defaultContractsMapTT? R
)TTR S
;TTS T
}UU 
}VV 	
staticXX 
voidXX "
ProcessSceneDecoratorsXX *
(XX* +
LoadedSceneInfoYY 
	sceneInfoYY %
,YY% &

DictionaryZZ 
<ZZ 
stringZZ 
,ZZ 
LoadedSceneInfoZZ .
>ZZ. /
contractMapZZ0 ;
,ZZ; <

Dictionary[[ 
<[[ 
string[[ 
,[[ 
string[[ %
>[[% &
defaultContractsMap[[' :
)[[: ;
{\\ 	
var]] !
decoratedContractName]] %
=]]& '
	sceneInfo]]( 1
.]]1 2
DecoratorContext]]2 B
.]]B C!
DecoratedContractName]]C X
;]]X Y
LoadedSceneInfo__ 
decoratedSceneInfo__ .
;__. /
ifaa 
(aa 
contractMapaa 
.aa 
TryGetValueaa '
(aa' (!
decoratedContractNameaa( =
,aa= >
outaa? B
decoratedSceneInfoaaC U
)aaU V
)aaV W
{bb '
ValidateDecoratedSceneMatchcc +
(cc+ ,
	sceneInfocc, 5
,cc5 6
decoratedSceneInfocc7 I
)ccI J
;ccJ K
returndd 
;dd 
}ee 
decoratedSceneInfogg 
=gg  '
LoadDefaultSceneForContractgg! <
(gg< =
	sceneInfohh 
,hh !
decoratedContractNamehh 0
,hh0 1
defaultContractsMaphh2 E
)hhE F
;hhF G
EditorSceneManagerjj 
.jj 
MoveSceneAfterjj -
(jj- .
decoratedSceneInfojj. @
.jj@ A
ScenejjA F
,jjF G
	sceneInfojjH Q
.jjQ R
ScenejjR W
)jjW X
;jjX Y'
ValidateDecoratedSceneMatchll '
(ll' (
	sceneInfoll( 1
,ll1 2
decoratedSceneInfoll3 E
)llE F
;llF G
ProcessScenenn 
(nn 
decoratedSceneInfonn +
,nn+ ,
contractMapnn- 8
,nn8 9
defaultContractsMapnn: M
)nnM N
;nnN O
}oo 	
staticqq 
voidqq 
ProcessSceneParentsqq '
(qq' (
LoadedSceneInforr 
	sceneInforr %
,rr% &

Dictionaryss 
<ss 
stringss 
,ss 
LoadedSceneInfoss .
>ss. /
contractMapss0 ;
,ss; <

Dictionarytt 
<tt 
stringtt 
,tt 
stringtt %
>tt% &
defaultContractsMaptt' :
)tt: ;
{uu 	
foreachvv 
(vv 
varvv 
parentContractNamevv +
invv, .
	sceneInfovv/ 8
.vv8 9
SceneContextvv9 E
.vvE F
ParentContractNamesvvF Y
)vvY Z
{ww 
LoadedSceneInfoxx 

parentInfoxx  *
;xx* +
ifzz 
(zz 
contractMapzz 
.zz  
TryGetValuezz  +
(zz+ ,
parentContractNamezz, >
,zz> ?
outzz@ C

parentInfozzD N
)zzN O
)zzO P
{{{ $
ValidateParentChildMatch|| ,
(||, -

parentInfo||- 7
,||7 8
	sceneInfo||9 B
)||B C
;||C D
continue}} 
;}} 
}~~ 

parentInfo
ÄÄ 
=
ÄÄ )
LoadDefaultSceneForContract
ÄÄ 8
(
ÄÄ8 9
	sceneInfo
ÄÄ9 B
,
ÄÄB C 
parentContractName
ÄÄD V
,
ÄÄV W!
defaultContractsMap
ÄÄX k
)
ÄÄk l
;
ÄÄl m
AddToContractMap
ÇÇ  
(
ÇÇ  !
contractMap
ÇÇ! ,
,
ÇÇ, -

parentInfo
ÇÇ. 8
)
ÇÇ8 9
;
ÇÇ9 : 
EditorSceneManager
ÑÑ "
.
ÑÑ" #
MoveSceneBefore
ÑÑ# 2
(
ÑÑ2 3

parentInfo
ÑÑ3 =
.
ÑÑ= >
Scene
ÑÑ> C
,
ÑÑC D
	sceneInfo
ÑÑE N
.
ÑÑN O
Scene
ÑÑO T
)
ÑÑT U
;
ÑÑU V&
ValidateParentChildMatch
ÜÜ (
(
ÜÜ( )

parentInfo
ÜÜ) 3
,
ÜÜ3 4
	sceneInfo
ÜÜ5 >
)
ÜÜ> ?
;
ÜÜ? @
ProcessScene
àà 
(
àà 

parentInfo
àà '
,
àà' (
contractMap
àà) 4
,
àà4 5!
defaultContractsMap
àà6 I
)
ààI J
;
ààJ K
}
ââ 
}
ää 	
static
åå 
LoadedSceneInfo
åå )
LoadDefaultSceneForContract
åå :
(
åå: ;
LoadedSceneInfo
çç 
	sceneInfo
çç %
,
çç% &
string
çç' -
contractName
çç. :
,
çç: ;

Dictionary
çç< F
<
ççF G
string
ççG M
,
ççM N
string
ççO U
>
ççU V!
defaultContractsMap
ççW j
)
ççj k
{
éé 	
string
èè 
	scenePath
èè 
;
èè 
if
ëë 
(
ëë 
!
ëë !
defaultContractsMap
ëë $
.
ëë$ %
TryGetValue
ëë% 0
(
ëë0 1
contractName
ëë1 =
,
ëë= >
out
ëë? B
	scenePath
ëëC L
)
ëëL M
)
ëëM N
{
íí 
throw
ìì 
Assert
ìì 
.
ìì 
CreateException
ìì ,
(
ìì, -
$strîî Õ
.
ïï 
Fmt
ïï 
(
ïï 
contractName
ïï %
,
ïï% &
	sceneInfo
ïï' 0
.
ïï0 1
Scene
ïï1 6
.
ïï6 7
name
ïï7 ;
)
ïï; <
)
ïï< =
;
ïï= >
}
ññ 
Scene
òò 
scene
òò 
;
òò 
try
öö 
{
õõ 
scene
úú 
=
úú  
EditorSceneManager
úú *
.
úú* +
	OpenScene
úú+ 4
(
úú4 5
	scenePath
úú5 >
,
úú> ?
OpenSceneMode
úú@ M
.
úúM N
Additive
úúN V
)
úúV W
;
úúW X
}
ùù 
catch
ûû 
(
ûû 
	Exception
ûû 
e
ûû 
)
ûû 
{
üü 
throw
†† 
new
†† 
ZenjectException
†† *
(
††* +
$str
°° N
.
°°N O
Fmt
°°O R
(
°°R S
	sceneInfo
°°S \
.
°°\ ]
Scene
°°] b
.
°°b c
name
°°c g
)
°°g h
,
°°h i
e
°°j k
)
°°k l
;
°°l m
}
¢¢ 
return
§§ #
CreateLoadedSceneInfo
§§ (
(
§§( )
scene
§§) .
)
§§. /
;
§§/ 0
}
•• 	
static
ßß 
void
ßß )
ValidateDecoratedSceneMatch
ßß /
(
ßß/ 0
LoadedSceneInfo
®® 
decoratorInfo
®® )
,
®®) *
LoadedSceneInfo
®®+ :
decoratedInfo
®®; H
)
®®H I
{
©© 	
var
™™ 
decoratorIndex
™™ 
=
™™  
GetSceneIndex
™™! .
(
™™. /
decoratorInfo
™™/ <
.
™™< =
Scene
™™= B
)
™™B C
;
™™C D
var
´´ 
decoratedIndex
´´ 
=
´´  
GetSceneIndex
´´! .
(
´´. /
decoratedInfo
´´/ <
.
´´< =
Scene
´´= B
)
´´B C
;
´´C D
var
¨¨ 
activeIndex
¨¨ 
=
¨¨ 
GetSceneIndex
¨¨ +
(
¨¨+ , 
EditorSceneManager
¨¨, >
.
¨¨> ?
GetActiveScene
¨¨? M
(
¨¨M N
)
¨¨N O
)
¨¨O P
;
¨¨P Q
Assert
ÆÆ 
.
ÆÆ 
That
ÆÆ 
(
ÆÆ 
decoratorIndex
ÆÆ &
<
ÆÆ' (
decoratedIndex
ÆÆ) 7
,
ÆÆ7 8
$strØØ ∞
,ØØ∞ ±
decoratorInfo
∞∞ 
.
∞∞ 
Scene
∞∞ #
.
∞∞# $
name
∞∞$ (
,
∞∞( )
decoratedInfo
∞∞* 7
.
∞∞7 8
Scene
∞∞8 =
.
∞∞= >
name
∞∞> B
)
∞∞B C
;
∞∞C D
if
≤≤ 
(
≤≤ 
activeIndex
≤≤ 
>
≤≤ 
decoratorIndex
≤≤ ,
)
≤≤, -
{
≥≥  
EditorSceneManager
¥¥ "
.
¥¥" #
SetActiveScene
¥¥# 1
(
¥¥1 2
decoratorInfo
¥¥2 ?
.
¥¥? @
Scene
¥¥@ E
)
¥¥E F
;
¥¥F G
}
µµ 
}
∂∂ 	
static
∏∏ 
void
∏∏ &
ValidateParentChildMatch
∏∏ ,
(
∏∏, -
LoadedSceneInfo
ππ 
parentSceneInfo
ππ +
,
ππ+ ,
LoadedSceneInfo
ππ- <
	sceneInfo
ππ= F
)
ππF G
{
∫∫ 	
var
ªª 
parentIndex
ªª 
=
ªª 
GetSceneIndex
ªª +
(
ªª+ ,
parentSceneInfo
ªª, ;
.
ªª; <
Scene
ªª< A
)
ªªA B
;
ªªB C
var
ºº 

childIndex
ºº 
=
ºº 
GetSceneIndex
ºº *
(
ºº* +
	sceneInfo
ºº+ 4
.
ºº4 5
Scene
ºº5 :
)
ºº: ;
;
ºº; <
var
ΩΩ 
activeIndex
ΩΩ 
=
ΩΩ 
GetSceneIndex
ΩΩ +
(
ΩΩ+ , 
EditorSceneManager
ΩΩ, >
.
ΩΩ> ?
GetActiveScene
ΩΩ? M
(
ΩΩM N
)
ΩΩN O
)
ΩΩO P
;
ΩΩP Q
Assert
øø 
.
øø 
That
øø 
(
øø 
parentIndex
øø #
<
øø$ %

childIndex
øø& 0
,
øø0 1
$str¿¿ í
,¿¿í ì
parentSceneInfo¿¿î £
.¿¿£ §
Scene¿¿§ ©
.¿¿© ™
name¿¿™ Æ
,¿¿Æ Ø
	sceneInfo¿¿∞ π
.¿¿π ∫
Scene¿¿∫ ø
.¿¿ø ¿
name¿¿¿ ƒ
)¿¿ƒ ≈
;¿¿≈ ∆
if
¬¬ 
(
¬¬ 
activeIndex
¬¬ 
>
¬¬ 
parentIndex
¬¬ )
)
¬¬) *
{
√√  
EditorSceneManager
ƒƒ "
.
ƒƒ" #
SetActiveScene
ƒƒ# 1
(
ƒƒ1 2
parentSceneInfo
ƒƒ2 A
.
ƒƒA B
Scene
ƒƒB G
)
ƒƒG H
;
ƒƒH I
}
≈≈ 
}
∆∆ 	
static
»» 
int
»» 
GetSceneIndex
»»  
(
»»  !
Scene
»»! &
scene
»»' ,
)
»», -
{
…… 	
for
   
(
   
int
   
i
   
=
   
$num
   
;
   
i
   
<
    
EditorSceneManager
    2
.
  2 3

sceneCount
  3 =
;
  = >
i
  ? @
++
  @ B
)
  B C
{
ÀÀ 
if
ÃÃ 
(
ÃÃ  
EditorSceneManager
ÃÃ &
.
ÃÃ& '

GetSceneAt
ÃÃ' 1
(
ÃÃ1 2
i
ÃÃ2 3
)
ÃÃ3 4
==
ÃÃ5 7
scene
ÃÃ8 =
)
ÃÃ= >
{
ÕÕ 
return
ŒŒ 
i
ŒŒ 
;
ŒŒ 
}
œœ 
}
–– 
throw
““ 
Assert
““ 
.
““ 
CreateException
““ (
(
““( )
)
““) *
;
““* +
}
”” 	
static
’’ 

Dictionary
’’ 
<
’’ 
string
’’  
,
’’  !
string
’’" (
>
’’( )%
LoadDefaultContractsMap
’’* A
(
’’A B
)
’’B C
{
÷÷ 	
var
◊◊ 
configs
◊◊ 
=
◊◊ 
	Resources
◊◊ #
.
◊◊# $
LoadAll
◊◊$ +
<
◊◊+ ,(
DefaultSceneContractConfig
◊◊, F
>
◊◊F G
(
◊◊G H(
DefaultSceneContractConfig
◊◊H b
.
◊◊b c
ResourcePath
◊◊c o
)
◊◊o p
;
◊◊p q
var
ŸŸ 
map
ŸŸ 
=
ŸŸ 
new
ŸŸ 

Dictionary
ŸŸ $
<
ŸŸ$ %
string
ŸŸ% +
,
ŸŸ+ ,
string
ŸŸ- 3
>
ŸŸ3 4
(
ŸŸ4 5
)
ŸŸ5 6
;
ŸŸ6 7
foreach
€€ 
(
€€ 
var
€€ 
config
€€ 
in
€€  "
configs
€€# *
)
€€* +
{
‹‹ 
foreach
›› 
(
›› 
var
›› 
info
›› !
in
››" $
config
››% +
.
››+ ,
DefaultContracts
››, <
)
››< =
{
ﬁﬁ 
if
ﬂﬂ 
(
ﬂﬂ 
info
ﬂﬂ 
.
ﬂﬂ 
ContractName
ﬂﬂ )
.
ﬂﬂ) *
Trim
ﬂﬂ* .
(
ﬂﬂ. /
)
ﬂﬂ/ 0
.
ﬂﬂ0 1
IsEmpty
ﬂﬂ1 8
(
ﬂﬂ8 9
)
ﬂﬂ9 :
)
ﬂﬂ: ;
{
‡‡ 
Log
·· 
.
·· 
Warn
··  
(
··  !
$str
··! k
,
··k l
AssetDatabase
··m z
.
··z {
GetAssetPath··{ á
(··á à
config··à é
)··é è
)··è ê
;··ê ë
continue
‚‚  
;
‚‚  !
}
„„ 
Assert
ÂÂ 
.
ÂÂ 
That
ÂÂ 
(
ÂÂ  
!
ÂÂ  !
map
ÂÂ! $
.
ÂÂ$ %
ContainsKey
ÂÂ% 0
(
ÂÂ0 1
info
ÂÂ1 5
.
ÂÂ5 6
ContractName
ÂÂ6 B
)
ÂÂB C
,
ÂÂC D
$strÊÊ á
,ÊÊá à
infoÊÊâ ç
.ÊÊç é
ContractNameÊÊé ö
,ÊÊö õ
AssetDatabaseÊÊú ©
.ÊÊ© ™
GetAssetPathÊÊ™ ∂
(ÊÊ∂ ∑
configÊÊ∑ Ω
)ÊÊΩ æ
)ÊÊæ ø
;ÊÊø ¿
map
ËË 
.
ËË 
Add
ËË 
(
ËË 
info
ËË  
.
ËË  !
ContractName
ËË! -
,
ËË- .
AssetDatabase
ËË/ <
.
ËË< =
GetAssetPath
ËË= I
(
ËËI J
info
ËËJ N
.
ËËN O
Scene
ËËO T
)
ËËT U
)
ËËU V
;
ËËV W
}
ÈÈ 
}
ÍÍ 
return
ÏÏ 
map
ÏÏ 
;
ÏÏ 
}
ÌÌ 	
static
ÔÔ 
LoadedSceneInfo
ÔÔ #
CreateLoadedSceneInfo
ÔÔ 4
(
ÔÔ4 5
Scene
ÔÔ5 :
scene
ÔÔ; @
)
ÔÔ@ A
{
 	
var
ÒÒ 
info
ÒÒ 
=
ÒÒ &
TryCreateLoadedSceneInfo
ÒÒ /
(
ÒÒ/ 0
scene
ÒÒ0 5
)
ÒÒ5 6
;
ÒÒ6 7
Assert
ÚÚ 
.
ÚÚ 
	IsNotNull
ÚÚ 
(
ÚÚ 
info
ÚÚ !
,
ÚÚ! "
$str
ÚÚ# O
,
ÚÚO P
scene
ÚÚQ V
.
ÚÚV W
name
ÚÚW [
)
ÚÚ[ \
;
ÚÚ\ ]
return
ÛÛ 
info
ÛÛ 
;
ÛÛ 
}
ÙÙ 	
static
ˆˆ 
LoadedSceneInfo
ˆˆ &
TryCreateLoadedSceneInfo
ˆˆ 7
(
ˆˆ7 8
Scene
ˆˆ8 =
scene
ˆˆ> C
)
ˆˆC D
{
˜˜ 	
var
¯¯ 
sceneContext
¯¯ 
=
¯¯  
ZenUnityEditorUtil
¯¯ 1
.
¯¯1 2(
TryGetSceneContextForScene
¯¯2 L
(
¯¯L M
scene
¯¯M R
)
¯¯R S
;
¯¯S T
var
˘˘ 
decoratorContext
˘˘  
=
˘˘! " 
ZenUnityEditorUtil
˘˘# 5
.
˘˘5 6,
TryGetDecoratorContextForScene
˘˘6 T
(
˘˘T U
scene
˘˘U Z
)
˘˘Z [
;
˘˘[ \
if
˚˚ 
(
˚˚ 
sceneContext
˚˚ 
==
˚˚ 
null
˚˚  $
&&
˚˚% '
decoratorContext
˚˚( 8
==
˚˚9 ;
null
˚˚< @
)
˚˚@ A
{
¸¸ 
return
˝˝ 
null
˝˝ 
;
˝˝ 
}
˛˛ 
var
ÄÄ 
info
ÄÄ 
=
ÄÄ 
new
ÄÄ 
LoadedSceneInfo
ÄÄ *
{
ÅÅ 
Scene
ÇÇ 
=
ÇÇ 
scene
ÇÇ 
}
ÉÉ 
;
ÉÉ 
if
ÖÖ 
(
ÖÖ 
sceneContext
ÖÖ 
!=
ÖÖ 
null
ÖÖ  $
)
ÖÖ$ %
{
ÜÜ 
Assert
áá 
.
áá 
IsNull
áá 
(
áá 
decoratorContext
áá .
,
áá. /
$str
àà R
,
ààR S
scene
ààT Y
.
ààY Z
name
ààZ ^
)
àà^ _
;
àà_ `
info
ää 
.
ää 
SceneContext
ää !
=
ää" #
sceneContext
ää$ 0
;
ää0 1
}
ãã 
else
åå 
{
çç 
Assert
éé 
.
éé 
	IsNotNull
éé  
(
éé  !
decoratorContext
éé! 1
)
éé1 2
;
éé2 3
info
êê 
.
êê 
DecoratorContext
êê %
=
êê& '
decoratorContext
êê( 8
;
êê8 9
}
ëë 
return
ìì 
info
ìì 
;
ìì 
}
îî 	
static
ññ 
List
ññ 
<
ññ 
LoadedSceneInfo
ññ #
>
ññ# $(
GetLoadedZenjectSceneInfos
ññ% ?
(
ññ? @
)
ññ@ A
{
óó 	
var
òò 
result
òò 
=
òò 
new
òò 
List
òò !
<
òò! "
LoadedSceneInfo
òò" 1
>
òò1 2
(
òò2 3
)
òò3 4
;
òò4 5
for
öö 
(
öö 
int
öö 
i
öö 
=
öö 
$num
öö 
;
öö 
i
öö 
<
öö  
EditorSceneManager
öö  2
.
öö2 3

sceneCount
öö3 =
;
öö= >
i
öö? @
++
öö@ B
)
ööB C
{
õõ 
var
úú 
scene
úú 
=
úú  
EditorSceneManager
úú .
.
úú. /

GetSceneAt
úú/ 9
(
úú9 :
i
úú: ;
)
úú; <
;
úú< =
var
ùù 
info
ùù 
=
ùù &
TryCreateLoadedSceneInfo
ùù 3
(
ùù3 4
scene
ùù4 9
)
ùù9 :
;
ùù: ;
if
üü 
(
üü 
info
üü 
!=
üü 
null
üü  
)
üü  !
{
†† 
result
°° 
.
°° 
Add
°° 
(
°° 
info
°° #
)
°°# $
;
°°$ %
}
¢¢ 
}
££ 
return
•• 
result
•• 
;
•• 
}
¶¶ 	
static
®® 
void
®® 
AddToContractMap
®® $
(
®®$ %

Dictionary
©© 
<
©© 
string
©© 
,
©© 
LoadedSceneInfo
©© .
>
©©. /
contractMap
©©0 ;
,
©©; <
LoadedSceneInfo
©©= L
info
©©M Q
)
©©Q R
{
™™ 	
if
´´ 
(
´´ 
info
´´ 
.
´´ 
SceneContext
´´ !
==
´´" $
null
´´% )
)
´´) *
{
¨¨ 
return
≠≠ 
;
≠≠ 
}
ÆÆ 
foreach
∞∞ 
(
∞∞ 
var
∞∞ 
contractName
∞∞ %
in
∞∞& (
info
∞∞) -
.
∞∞- .
SceneContext
∞∞. :
.
∞∞: ;
ContractNames
∞∞; H
)
∞∞H I
{
±± 
LoadedSceneInfo
≤≤ 
currentInfo
≤≤  +
;
≤≤+ ,
if
¥¥ 
(
¥¥ 
contractMap
¥¥ 
.
¥¥  
TryGetValue
¥¥  +
(
¥¥+ ,
contractName
¥¥, 8
,
¥¥8 9
out
¥¥: =
currentInfo
¥¥> I
)
¥¥I J
)
¥¥J K
{
µµ 
throw
∂∂ 
Assert
∂∂  
.
∂∂  !
CreateException
∂∂! 0
(
∂∂0 1
$str
∑∑ e
,
∑∑e f
contractName
∏∏ $
,
∏∏$ %
currentInfo
∏∏& 1
.
∏∏1 2
Scene
∏∏2 7
.
∏∏7 8
name
∏∏8 <
,
∏∏< =
info
∏∏> B
.
∏∏B C
Scene
∏∏C H
.
∏∏H I
name
∏∏I M
)
∏∏M N
;
∏∏N O
}
ππ 
contractMap
ªª 
.
ªª 
Add
ªª 
(
ªª  
contractName
ªª  ,
,
ªª, -
info
ªª. 2
)
ªª2 3
;
ªª3 4
}
ºº 
}
ΩΩ 	
public
øø 
class
øø 
LoadedSceneInfo
øø $
{
¿¿ 	
public
¡¡ 
SceneContext
¡¡ 
SceneContext
¡¡  ,
;
¡¡, -
public
¬¬ #
SceneDecoratorContext
¬¬ (
DecoratorContext
¬¬) 9
;
¬¬9 :
public
√√ 
Scene
√√ 
Scene
√√ 
;
√√ 
}
ƒƒ 	
}
≈≈ 
}∆∆ ıÃ
\C:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\ZenUnityEditorUtil.cs
	namespace 	
Zenject
 
. 
Internal 
{ 
public 

static 
class 
ZenUnityEditorUtil *
{ 
public 
static 
bool )
SaveThenRunPreserveSceneSetup 8
(8 9
Action9 ?
action@ F
)F G
{ 	
if 
( 
EditorSceneManager "
." #2
&SaveCurrentModifiedScenesIfUserWantsTo# I
(I J
)J K
)K L
{ 
var 
originalSceneSetup &
=' (
EditorSceneManager) ;
.; < 
GetSceneManagerSetup< P
(P Q
)Q R
;R S
try 
{ 
action 
( 
) 
; 
return 
true 
;  
} 
catch 
( 
	Exception  
e! "
)" #
{ 
Log 
. 
ErrorException &
(& '
e' (
)( )
;) *
return   
false    
;    !
}!! 
finally"" 
{## 
EditorSceneManager$$ &
.$$& '$
RestoreSceneManagerSetup$$' ?
($$? @
originalSceneSetup$$@ R
)$$R S
;$$S T
}%% 
}&& 
return(( 
false(( 
;(( 
})) 	
public-- 
static-- 
void-- %
ValidateCurrentSceneSetup-- 4
(--4 5
)--5 6
{.. 	
bool// 
encounteredError// !
=//" #
false//$ )
;//) *
Application11 
.11 
LogCallback11 #
logCallback11$ /
=110 1
(112 3
	condition113 <
,11< =

stackTrace11> H
,11H I
type11J N
)11N O
=>11P R
{22 
if33 
(33 
type33 
==33 
LogType33 #
.33# $
Error33$ )
||33* ,
type33- 1
==332 4
LogType335 <
.33< =
Assert33= C
||44 
type44 
==44 
LogType44 &
.44& '
	Exception44' 0
)440 1
{55 
encounteredError66 $
=66% &
true66' +
;66+ ,
}77 
}88 
;88 
Application:: 
.:: 
logMessageReceived:: *
+=::+ -
logCallback::. 9
;::9 :
try<< 
{== 
Assert>> 
.>> 
That>> 
(>> 
!>> 
ProjectContext>> +
.>>+ ,
HasInstance>>, 7
)>>7 8
;>>8 9
ProjectContext?? 
.?? 
ValidateOnNextRun?? 0
=??1 2
true??3 7
;??7 8
foreachAA 
(AA 
varAA 
sceneContextAA )
inAA* ,
GetAllSceneContextsAA- @
(AA@ A
)AAA B
)AAB C
{BB 
sceneContextCC  
.CC  !
ValidateCC! )
(CC) *
)CC* +
;CC+ ,
}DD 
}EE 
catchFF 
(FF 
	ExceptionFF 
eFF 
)FF 
{GG 
LogHH 
.HH 
ErrorExceptionHH "
(HH" #
eHH# $
)HH$ %
;HH% &
encounteredErrorII  
=II! "
trueII# '
;II' (
}JJ 
finallyKK 
{LL 
ApplicationMM 
.MM 
logMessageReceivedMM .
-=MM/ 1
logCallbackMM2 =
;MM= >
}NN 
ifPP 
(PP 
encounteredErrorPP  
)PP  !
{QQ 
throwRR 
newRR 
ZenjectExceptionRR *
(RR* +
$strRR+ f
)RRf g
;RRg h
}SS 
}TT 	
publicXX 
staticXX 
intXX #
ValidateAllActiveScenesXX 1
(XX1 2
)XX2 3
{YY 	
varZZ 
activeScenePathsZZ  
=ZZ! "
EditorBuildSettingsZZ# 6
.ZZ6 7
scenesZZ7 =
.ZZ= >
WhereZZ> C
(ZZC D
xZZD E
=>ZZF H
xZZI J
.ZZJ K
enabledZZK R
)ZZR S
.[[ 
Select[[ 
([[ 
x[[ 
=>[[ 
x[[ 
.[[ 
path[[ #
)[[# $
.[[$ %
ToList[[% +
([[+ ,
)[[, -
;[[- .
foreach]] 
(]] 
var]] 
	scenePath]] "
in]]# %
activeScenePaths]]& 6
)]]6 7
{^^ 
EditorSceneManager__ "
.__" #
	OpenScene__# ,
(__, -
	scenePath__- 6
,__6 7
OpenSceneMode__8 E
.__E F
Single__F L
)__L M
;__M N%
ValidateCurrentSceneSetup`` )
(``) *
)``* +
;``+ ,
}aa 
returncc 
activeScenePathscc #
.cc# $
Countcc$ )
;cc) *
}dd 	
publicgg 
staticgg 
voidgg  
RunCurrentSceneSetupgg /
(gg/ 0
)gg0 1
{hh 	
Assertii 
.ii 
Thatii 
(ii 
!ii 
ProjectContextii '
.ii' (
HasInstanceii( 3
)ii3 4
;ii4 5
foreachkk 
(kk 
varkk 
sceneContextkk %
inkk& (
GetAllSceneContextskk) <
(kk< =
)kk= >
)kk> ?
{ll 
trymm 
{nn 
sceneContextoo  
.oo  !
Runoo! $
(oo$ %
)oo% &
;oo& '
}pp 
catchqq 
(qq 
	Exceptionqq  
eqq! "
)qq" #
{rr 
throwtt 
newtt 
ZenjectExceptiontt .
(tt. /
$struu 6
.uu6 7
Fmtuu7 :
(uu: ;
sceneContextuu; G
.uuG H

gameObjectuuH R
.uuR S
sceneuuS X
.uuX Y
nameuuY ]
)uu] ^
,uu^ _
euu` a
)uua b
;uub c
}vv 
}ww 
}xx 	
publiczz 
staticzz 
SceneContextzz "#
GetSceneContextForScenezz# :
(zz: ;
Scenezz; @
scenezzA F
)zzF G
{{{ 	
var|| 
sceneContext|| 
=|| &
TryGetSceneContextForScene|| 9
(||9 :
scene||: ?
)||? @
;||@ A
Assert~~ 
.~~ 
	IsNotNull~~ 
(~~ 
sceneContext~~ )
,~~) *
$str >
,> ?
scene@ E
.E F
nameF J
)J K
;K L
return
ÅÅ 
sceneContext
ÅÅ 
;
ÅÅ  
}
ÇÇ 	
public
ÑÑ 
static
ÑÑ 
SceneContext
ÑÑ "(
TryGetSceneContextForScene
ÑÑ# =
(
ÑÑ= >
Scene
ÑÑ> C
scene
ÑÑD I
)
ÑÑI J
{
ÖÖ 	
if
ÜÜ 
(
ÜÜ 
!
ÜÜ 
scene
ÜÜ 
.
ÜÜ 
isLoaded
ÜÜ 
)
ÜÜ  
{
áá 
return
àà 
null
àà 
;
àà 
}
ââ 
var
ãã 
sceneContexts
ãã 
=
ãã 
scene
ãã  %
.
ãã% & 
GetRootGameObjects
ãã& 8
(
ãã8 9
)
ãã9 :
.
åå 

SelectMany
åå 
(
åå 
x
åå 
=>
åå  
x
åå! "
.
åå" #%
GetComponentsInChildren
åå# :
<
åå: ;
SceneContext
åå; G
>
ååG H
(
ååH I
)
ååI J
)
ååJ K
.
ååK L
ToList
ååL R
(
ååR S
)
ååS T
;
ååT U
if
éé 
(
éé 
sceneContexts
éé 
.
éé 
IsEmpty
éé %
(
éé% &
)
éé& '
)
éé' (
{
èè 
return
êê 
null
êê 
;
êê 
}
ëë 
Assert
ìì 
.
ìì 
That
ìì 
(
ìì 
sceneContexts
ìì %
.
ìì% &
Count
ìì& +
==
ìì, .
$num
ìì/ 0
,
ìì0 1
$str
îî Z
,
îîZ [
scene
îî\ a
.
îîa b
name
îîb f
)
îîf g
;
îîg h
return
ññ 
sceneContexts
ññ  
[
ññ  !
$num
ññ! "
]
ññ" #
;
ññ# $
}
óó 	
public
ôô 
static
ôô #
SceneDecoratorContext
ôô +)
GetDecoratorContextForScene
ôô, G
(
ôôG H
Scene
ôôH M
scene
ôôN S
)
ôôS T
{
öö 	
var
õõ 
decoratorContext
õõ  
=
õõ! ",
TryGetDecoratorContextForScene
õõ# A
(
õõA B
scene
õõB G
)
õõG H
;
õõH I
Assert
ùù 
.
ùù 
	IsNotNull
ùù 
(
ùù 
decoratorContext
ùù -
,
ùù- .
$str
ûû B
,
ûûB C
scene
ûûD I
.
ûûI J
name
ûûJ N
)
ûûN O
;
ûûO P
return
†† 
decoratorContext
†† #
;
††# $
}
°° 	
public
££ 
static
££ #
SceneDecoratorContext
££ +,
TryGetDecoratorContextForScene
££, J
(
££J K
Scene
££K P
scene
££Q V
)
££V W
{
§§ 	
if
•• 
(
•• 
!
•• 
scene
•• 
.
•• 
isLoaded
•• 
)
••  
{
¶¶ 
return
ßß 
null
ßß 
;
ßß 
}
®® 
var
™™ 
decoratorContexts
™™ !
=
™™" #
scene
™™$ )
.
™™) * 
GetRootGameObjects
™™* <
(
™™< =
)
™™= >
.
´´ 

SelectMany
´´ 
(
´´ 
x
´´ 
=>
´´  
x
´´! "
.
´´" #%
GetComponentsInChildren
´´# :
<
´´: ;#
SceneDecoratorContext
´´; P
>
´´P Q
(
´´Q R
)
´´R S
)
´´S T
.
´´T U
ToList
´´U [
(
´´[ \
)
´´\ ]
;
´´] ^
if
≠≠ 
(
≠≠ 
decoratorContexts
≠≠ !
.
≠≠! "
IsEmpty
≠≠" )
(
≠≠) *
)
≠≠* +
)
≠≠+ ,
{
ÆÆ 
return
ØØ 
null
ØØ 
;
ØØ 
}
∞∞ 
Assert
≤≤ 
.
≤≤ 
That
≤≤ 
(
≤≤ 
decoratorContexts
≤≤ )
.
≤≤) *
Count
≤≤* /
==
≤≤0 2
$num
≤≤3 4
,
≤≤4 5
$str
≥≥ ^
,
≥≥^ _
scene
≥≥` e
.
≥≥e f
name
≥≥f j
)
≥≥j k
;
≥≥k l
return
µµ 
decoratorContexts
µµ $
[
µµ$ %
$num
µµ% &
]
µµ& '
;
µµ' (
}
∂∂ 	
static
∏∏ 
IEnumerable
∏∏ 
<
∏∏ 
SceneContext
∏∏ '
>
∏∏' (!
GetAllSceneContexts
∏∏) <
(
∏∏< =
)
∏∏= >
{
ππ 	
var
∫∫ !
decoratedSceneNames
∫∫ #
=
∫∫$ %
new
∫∫& )
List
∫∫* .
<
∫∫. /
string
∫∫/ 5
>
∫∫5 6
(
∫∫6 7
)
∫∫7 8
;
∫∫8 9
for
ºº 
(
ºº 
int
ºº 
i
ºº 
=
ºº 
$num
ºº 
;
ºº 
i
ºº 
<
ºº  
EditorSceneManager
ºº  2
.
ºº2 3

sceneCount
ºº3 =
;
ºº= >
i
ºº? @
++
ºº@ B
)
ººB C
{
ΩΩ 
var
ææ 
scene
ææ 
=
ææ  
EditorSceneManager
ææ .
.
ææ. /

GetSceneAt
ææ/ 9
(
ææ9 :
i
ææ: ;
)
ææ; <
;
ææ< =
var
¿¿ 
sceneContext
¿¿  
=
¿¿! "(
TryGetSceneContextForScene
¿¿# =
(
¿¿= >
scene
¿¿> C
)
¿¿C D
;
¿¿D E
var
¡¡ 
decoratorContext
¡¡ $
=
¡¡% &,
TryGetDecoratorContextForScene
¡¡' E
(
¡¡E F
scene
¡¡F K
)
¡¡K L
;
¡¡L M
if
√√ 
(
√√ 
sceneContext
√√  
!=
√√! #
null
√√$ (
)
√√( )
{
ƒƒ 
Assert
≈≈ 
.
≈≈ 
That
≈≈ 
(
≈≈  
decoratorContext
≈≈  0
==
≈≈1 3
null
≈≈4 8
,
≈≈8 9
$str
∆∆ y
,
∆∆y z
scene∆∆{ Ä
.∆∆Ä Å
name∆∆Å Ö
)∆∆Ö Ü
;∆∆Ü á!
decoratedSceneNames
»» '
.
»»' (
	RemoveAll
»»( 1
(
»»1 2
x
»»2 3
=>
»»4 6
sceneContext
»»7 C
.
»»C D
ContractNames
»»D Q
.
»»Q R
Contains
»»R Z
(
»»Z [
x
»»[ \
)
»»\ ]
)
»»] ^
;
»»^ _
yield
   
return
    
sceneContext
  ! -
;
  - .
}
ÀÀ 
else
ÃÃ 
if
ÃÃ 
(
ÃÃ 
decoratorContext
ÃÃ )
!=
ÃÃ* ,
null
ÃÃ- 1
)
ÃÃ1 2
{
ÕÕ 
Assert
ŒŒ 
.
ŒŒ 
That
ŒŒ 
(
ŒŒ  
!
ŒŒ  !
string
ŒŒ! '
.
ŒŒ' (
IsNullOrEmpty
ŒŒ( 5
(
ŒŒ5 6
decoratorContext
ŒŒ6 F
.
ŒŒF G#
DecoratedContractName
ŒŒG \
)
ŒŒ\ ]
,
ŒŒ] ^
$str
œœ a
,
œœa b
scene
œœc h
.
œœh i
name
œœi m
)
œœm n
;
œœn o!
decoratedSceneNames
—— '
.
——' (
Add
——( +
(
——+ ,
decoratorContext
——, <
.
——< =#
DecoratedContractName
——= R
)
——R S
;
——S T
}
““ 
}
”” 
Assert
’’ 
.
’’ 
That
’’ 
(
’’ !
decoratedSceneNames
’’ +
.
’’+ ,
IsEmpty
’’, 3
(
’’3 4
)
’’4 5
,
’’5 6
$str
÷÷ r
,
÷÷r s"
decoratedSceneNames÷÷t á
.÷÷á à
Join÷÷à å
(÷÷å ç
$str÷÷ç ë
)÷÷ë í
)÷÷í ì
;÷÷ì î
}
◊◊ 	
public
ŸŸ 
static
ŸŸ 
string
ŸŸ ,
ConvertAssetPathToAbsolutePath
ŸŸ ;
(
ŸŸ; <
string
ŸŸ< B
	assetPath
ŸŸC L
)
ŸŸL M
{
⁄⁄ 	
return
€€ 
Path
€€ 
.
€€ 
Combine
€€ 
(
€€  
Path
‹‹ 
.
‹‹ 
Combine
‹‹ 
(
‹‹ 
Path
‹‹ !
.
‹‹! "
GetFullPath
‹‹" -
(
‹‹- .
Application
‹‹. 9
.
‹‹9 :
dataPath
‹‹: B
)
‹‹B C
,
‹‹C D
$str
‹‹E I
)
‹‹I J
,
‹‹J K
	assetPath
‹‹L U
)
‹‹U V
;
‹‹V W
}
›› 	
public
ﬂﬂ 
static
ﬂﬂ 
string
ﬂﬂ 0
"ConvertFullAbsolutePathToAssetPath
ﬂﬂ ?
(
ﬂﬂ? @
string
ﬂﬂ@ F
fullPath
ﬂﬂG O
)
ﬂﬂO P
{
‡‡ 	
fullPath
·· 
=
·· 
Path
·· 
.
·· 
GetFullPath
·· '
(
··' (
fullPath
··( 0
)
··0 1
;
··1 2
var
„„ !
assetFolderFullPath
„„ #
=
„„$ %
Path
„„& *
.
„„* +
GetFullPath
„„+ 6
(
„„6 7
Application
„„7 B
.
„„B C
dataPath
„„C K
)
„„K L
;
„„L M
if
ÂÂ 
(
ÂÂ 
fullPath
ÂÂ 
.
ÂÂ 
Length
ÂÂ 
==
ÂÂ  "!
assetFolderFullPath
ÂÂ# 6
.
ÂÂ6 7
Length
ÂÂ7 =
)
ÂÂ= >
{
ÊÊ 
Assert
ÁÁ 
.
ÁÁ 
IsEqual
ÁÁ 
(
ÁÁ 
fullPath
ÁÁ '
,
ÁÁ' (!
assetFolderFullPath
ÁÁ) <
)
ÁÁ< =
;
ÁÁ= >
return
ËË 
$str
ËË 
;
ËË  
}
ÈÈ 
var
ÎÎ 
	assetPath
ÎÎ 
=
ÎÎ 
fullPath
ÎÎ $
.
ÎÎ$ %
Remove
ÎÎ% +
(
ÎÎ+ ,
$num
ÎÎ, -
,
ÎÎ- .!
assetFolderFullPath
ÎÎ/ B
.
ÎÎB C
Length
ÎÎC I
+
ÎÎJ K
$num
ÎÎL M
)
ÎÎM N
.
ÎÎN O
Replace
ÎÎO V
(
ÎÎV W
$str
ÎÎW [
,
ÎÎ[ \
$str
ÎÎ] `
)
ÎÎ` a
;
ÎÎa b
return
ÏÏ 
$str
ÏÏ 
+
ÏÏ 
	assetPath
ÏÏ (
;
ÏÏ( )
}
ÌÌ 	
public
ÔÔ 
static
ÔÔ 
string
ÔÔ 7
)GetCurrentDirectoryAssetPathFromSelection
ÔÔ F
(
ÔÔF G
)
ÔÔG H
{
 	
return
ÒÒ 0
"ConvertFullAbsolutePathToAssetPath
ÒÒ 5
(
ÒÒ5 6:
,GetCurrentDirectoryAbsolutePathFromSelection
ÚÚ <
(
ÚÚ< =
)
ÚÚ= >
)
ÚÚ> ?
;
ÚÚ? @
}
ÛÛ 	
public
ıı 
static
ıı 
string
ıı :
,GetCurrentDirectoryAbsolutePathFromSelection
ıı I
(
ııI J
)
ııJ K
{
ˆˆ 	
var
˜˜ 

folderPath
˜˜ 
=
˜˜ 3
%TryGetSelectedFolderPathInProjectsTab
˜˜ B
(
˜˜B C
)
˜˜C D
;
˜˜D E
if
˘˘ 
(
˘˘ 

folderPath
˘˘ 
!=
˘˘ 
null
˘˘ "
)
˘˘" #
{
˙˙ 
return
˚˚ 

folderPath
˚˚ !
;
˚˚! "
}
¸¸ 
var
˛˛ 
filePath
˛˛ 
=
˛˛ 1
#TryGetSelectedFilePathInProjectsTab
˛˛ >
(
˛˛> ?
)
˛˛? @
;
˛˛@ A
if
ÄÄ 
(
ÄÄ 
filePath
ÄÄ 
!=
ÄÄ 
null
ÄÄ  
)
ÄÄ  !
{
ÅÅ 
return
ÇÇ 
Path
ÇÇ 
.
ÇÇ 
GetDirectoryName
ÇÇ ,
(
ÇÇ, -
filePath
ÇÇ- 5
)
ÇÇ5 6
;
ÇÇ6 7
}
ÉÉ 
return
ÖÖ 
Application
ÖÖ 
.
ÖÖ 
dataPath
ÖÖ '
;
ÖÖ' (
}
ÜÜ 	
public
àà 
static
àà 
string
àà 1
#TryGetSelectedFilePathInProjectsTab
àà @
(
àà@ A
)
ààA B
{
ââ 	
return
ää /
!GetSelectedFilePathsInProjectsTab
ää 4
(
ää4 5
)
ää5 6
.
ää6 7
OnlyOrDefault
ää7 D
(
ääD E
)
ääE F
;
ääF G
}
ãã 	
public
çç 
static
çç 
List
çç 
<
çç 
string
çç !
>
çç! "/
!GetSelectedFilePathsInProjectsTab
çç# D
(
ççD E
)
ççE F
{
éé 	
return
èè +
GetSelectedPathsInProjectsTab
èè 0
(
èè0 1
)
èè1 2
.
êê 
Where
êê 
(
êê 
x
êê 
=>
êê 
File
êê  
.
êê  !
Exists
êê! '
(
êê' (
x
êê( )
)
êê) *
)
êê* +
.
êê+ ,
ToList
êê, 2
(
êê2 3
)
êê3 4
;
êê4 5
}
ëë 	
public
ìì 
static
ìì 
List
ìì 
<
ìì 
string
ìì !
>
ìì! "0
"GetSelectedAssetPathsInProjectsTab
ìì# E
(
ììE F
)
ììF G
{
îî 	
var
ïï 
paths
ïï 
=
ïï 
new
ïï 
List
ïï  
<
ïï  !
string
ïï! '
>
ïï' (
(
ïï( )
)
ïï) *
;
ïï* +
UnityEngine
óó 
.
óó 
Object
óó 
[
óó 
]
óó  
selectedAssets
óó! /
=
óó0 1
	Selection
óó2 ;
.
óó; <
GetFiltered
óó< G
(
óóG H
typeof
òò 
(
òò 
UnityEngine
òò "
.
òò" #
Object
òò# )
)
òò) *
,
òò* +
SelectionMode
òò, 9
.
òò9 :
Assets
òò: @
)
òò@ A
;
òòA B
foreach
öö 
(
öö 
var
öö 
item
öö 
in
öö  
selectedAssets
öö! /
)
öö/ 0
{
õõ 
var
úú 
	assetPath
úú 
=
úú 
AssetDatabase
úú  -
.
úú- .
GetAssetPath
úú. :
(
úú: ;
item
úú; ?
)
úú? @
;
úú@ A
if
ûû 
(
ûû 
!
ûû 
string
ûû 
.
ûû 
IsNullOrEmpty
ûû )
(
ûû) *
	assetPath
ûû* 3
)
ûû3 4
)
ûû4 5
{
üü 
paths
†† 
.
†† 
Add
†† 
(
†† 
	assetPath
†† '
)
††' (
;
††( )
}
°° 
}
¢¢ 
return
§§ 
paths
§§ 
;
§§ 
}
•• 	
public
ßß 
static
ßß 
List
ßß 
<
ßß 
string
ßß !
>
ßß! "+
GetSelectedPathsInProjectsTab
ßß# @
(
ßß@ A
)
ßßA B
{
®® 	
var
©© 
paths
©© 
=
©© 
new
©© 
List
©©  
<
©©  !
string
©©! '
>
©©' (
(
©©( )
)
©©) *
;
©©* +
UnityEngine
´´ 
.
´´ 
Object
´´ 
[
´´ 
]
´´  
selectedAssets
´´! /
=
´´0 1
	Selection
´´2 ;
.
´´; <
GetFiltered
´´< G
(
´´G H
typeof
¨¨ 
(
¨¨ 
UnityEngine
¨¨ "
.
¨¨" #
Object
¨¨# )
)
¨¨) *
,
¨¨* +
SelectionMode
¨¨, 9
.
¨¨9 :
Assets
¨¨: @
)
¨¨@ A
;
¨¨A B
foreach
ÆÆ 
(
ÆÆ 
var
ÆÆ 
item
ÆÆ 
in
ÆÆ  
selectedAssets
ÆÆ! /
)
ÆÆ/ 0
{
ØØ 
var
∞∞ 
relativePath
∞∞  
=
∞∞! "
AssetDatabase
∞∞# 0
.
∞∞0 1
GetAssetPath
∞∞1 =
(
∞∞= >
item
∞∞> B
)
∞∞B C
;
∞∞C D
if
≤≤ 
(
≤≤ 
!
≤≤ 
string
≤≤ 
.
≤≤ 
IsNullOrEmpty
≤≤ )
(
≤≤) *
relativePath
≤≤* 6
)
≤≤6 7
)
≤≤7 8
{
≥≥ 
var
¥¥ 
fullPath
¥¥  
=
¥¥! "
Path
¥¥# '
.
¥¥' (
GetFullPath
¥¥( 3
(
¥¥3 4
Path
¥¥4 8
.
¥¥8 9
Combine
¥¥9 @
(
¥¥@ A
Application
µµ #
.
µµ# $
dataPath
µµ$ ,
,
µµ, -
Path
µµ. 2
.
µµ2 3
Combine
µµ3 :
(
µµ: ;
$str
µµ; ?
,
µµ? @
relativePath
µµA M
)
µµM N
)
µµN O
)
µµO P
;
µµP Q
paths
∑∑ 
.
∑∑ 
Add
∑∑ 
(
∑∑ 
fullPath
∑∑ &
)
∑∑& '
;
∑∑' (
}
∏∏ 
}
ππ 
return
ªª 
paths
ªª 
;
ªª 
}
ºº 	
public
øø 
static
øø 
void
øø '
SaveScriptableObjectAsset
øø 4
(
øø4 5
string
¿¿ 
path
¿¿ 
,
¿¿ 
ScriptableObject
¿¿ )
asset
¿¿* /
)
¿¿/ 0
{
¡¡ 	
Assert
¬¬ 
.
¬¬ 
That
¬¬ 
(
¬¬ 
path
¬¬ 
.
¬¬ 
EndsWith
¬¬ %
(
¬¬% &
$str
¬¬& .
)
¬¬. /
)
¬¬/ 0
;
¬¬0 1
string
ƒƒ 
assetPathAndName
ƒƒ #
=
ƒƒ$ %
AssetDatabase
ƒƒ& 3
.
ƒƒ3 4%
GenerateUniqueAssetPath
ƒƒ4 K
(
ƒƒK L
path
ƒƒL P
)
ƒƒP Q
;
ƒƒQ R
AssetDatabase
∆∆ 
.
∆∆ 
CreateAsset
∆∆ %
(
∆∆% &
asset
∆∆& +
,
∆∆+ ,
assetPathAndName
∆∆- =
)
∆∆= >
;
∆∆> ?
AssetDatabase
»» 
.
»» 

SaveAssets
»» $
(
»»$ %
)
»»% &
;
»»& '
AssetDatabase
…… 
.
…… 
Refresh
…… !
(
……! "
)
……" #
;
……# $
EditorUtility
   
.
    
FocusProjectWindow
   ,
(
  , -
)
  - .
;
  . /
	Selection
ÀÀ 
.
ÀÀ 
activeObject
ÀÀ "
=
ÀÀ# $
asset
ÀÀ% *
;
ÀÀ* +
}
ÃÃ 	
public
œœ 
static
œœ 
List
œœ 
<
œœ 
string
œœ !
>
œœ! "1
#GetSelectedFolderPathsInProjectsTab
œœ# F
(
œœF G
)
œœG H
{
–– 	
return
—— +
GetSelectedPathsInProjectsTab
—— 0
(
——0 1
)
——1 2
.
““ 
Where
““ 
(
““ 
x
““ 
=>
““ 
	Directory
““ %
.
““% &
Exists
““& ,
(
““, -
x
““- .
)
““. /
)
““/ 0
.
““0 1
ToList
““1 7
(
““7 8
)
““8 9
;
““9 :
}
”” 	
public
ŸŸ 
static
ŸŸ 
string
ŸŸ 3
%TryGetSelectedFolderPathInProjectsTab
ŸŸ B
(
ŸŸB C
)
ŸŸC D
{
⁄⁄ 	
return
€€ 1
#GetSelectedFolderPathsInProjectsTab
€€ 6
(
€€6 7
)
€€7 8
.
€€8 9
OnlyOrDefault
€€9 F
(
€€F G
)
€€G H
;
€€H I
}
‹‹ 	
}
›› 
}ﬁﬁ à
iC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\GameObjectContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( 
GameObjectContext *
)* +
)+ ,
], -
[ 
NoReflectionBaking 
] 
public		 

class		 #
GameObjectContextEditor		 (
:		) *!
RunnableContextEditor		+ @
{

 
SerializedProperty 
_kernel "
;" #
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_kernel 
= 
serializedObject &
.& '
FindProperty' 3
(3 4
$str4 =
)= >
;> ?
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_kernel* 1
)1 2
;2 3
} 	
} 
} ÿ	
gC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\RunnableContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
NoReflectionBaking 
] 
public 

class !
RunnableContextEditor &
:' (
ContextEditor) 6
{		 
SerializedProperty

 
_autoRun

 #
;

# $
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_autoRun 
= 
serializedObject '
.' (
FindProperty( 4
(4 5
$str5 ?
)? @
;@ A
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_autoRun* 2
)2 3
;3 4
} 	
} 
} è∫
VC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\ZenMenuItems.cs
	namespace		 	
Zenject		
 
.		 
Internal		 
{

 
public 

static 
class 
ZenMenuItems $
{ 
[ 	
MenuItem	 
( 
$str <
)< =
]= >
public 
static 
void  
ValidateCurrentScene /
(/ 0
)0 1
{ 	(
ValidateCurrentSceneInternal (
(( )
)) *
;* +
} 	
[ 	
MenuItem	 
( 
$str 6
)6 7
]7 8
public 
static 
void '
ValidateCurrentSceneThenRun 6
(6 7
)7 8
{ 	
if 
( (
ValidateCurrentSceneInternal ,
(, -
)- .
). /
{ 
EditorApplication !
.! "
	isPlaying" +
=, -
true. 2
;2 3
} 
} 	
[ 	
MenuItem	 
( 
$str (
)( )
]) *
public 
static 
void 
OpenDocumentation ,
(, -
)- .
{   	
Application!! 
.!! 
OpenURL!! 
(!!  
$str!!  G
)!!G H
;!!H I
}"" 	
[$$ 	
MenuItem$$	 
($$ 
$str$$ 4
,$$4 5
false$$6 ;
,$$; <
$num$$= >
)$$> ?
]$$? @
public%% 
static%% 
void%% 
CreateSceneContext%% -
(%%- .
MenuCommand%%. 9
menuCommand%%: E
)%%E F
{&& 	
var'' 
root'' 
='' 
new'' 

GameObject'' %
(''% &
$str''& 4
)''4 5
.''5 6
AddComponent''6 B
<''B C
SceneContext''C O
>''O P
(''P Q
)''Q R
;''R S
	Selection(( 
.(( 
activeGameObject(( &
=((' (
root(() -
.((- .

gameObject((. 8
;((8 9
EditorSceneManager** 
.** 
MarkSceneDirty** -
(**- .
EditorSceneManager**. @
.**@ A
GetActiveScene**A O
(**O P
)**P Q
)**Q R
;**R S
}++ 	
[-- 	
MenuItem--	 
(-- 
$str-- 8
,--8 9
false--: ?
,--? @
$num--A B
)--B C
]--C D
public.. 
static.. 
void.. "
CreateDecoratorContext.. 1
(..1 2
MenuCommand..2 =
menuCommand..> I
)..I J
{// 	
var00 
root00 
=00 
new00 

GameObject00 %
(00% &
$str00& 8
)008 9
.009 :
AddComponent00: F
<00F G!
SceneDecoratorContext00G \
>00\ ]
(00] ^
)00^ _
;00_ `
	Selection11 
.11 
activeGameObject11 &
=11' (
root11) -
.11- .

gameObject11. 8
;118 9
EditorSceneManager33 
.33 
MarkSceneDirty33 -
(33- .
EditorSceneManager33. @
.33@ A
GetActiveScene33A O
(33O P
)33P Q
)33Q R
;33R S
}44 	
[66 	
MenuItem66	 
(66 
$str66 :
,66: ;
false66< A
,66A B
$num66C D
)66D E
]66E F
public77 
static77 
void77 #
CreateGameObjectContext77 2
(772 3
MenuCommand773 >
menuCommand77? J
)77J K
{88 	
var99 
root99 
=99 
new99 

GameObject99 %
(99% &
$str99& 9
)999 :
.99: ;
AddComponent99; G
<99G H
GameObjectContext99H Y
>99Y Z
(99Z [
)99[ \
;99\ ]
	Selection:: 
.:: 
activeGameObject:: &
=::' (
root::) -
.::- .

gameObject::. 8
;::8 9
EditorSceneManager<< 
.<< 
MarkSceneDirty<< -
(<<- .
EditorSceneManager<<. @
.<<@ A
GetActiveScene<<A O
(<<O P
)<<P Q
)<<Q R
;<<R S
}== 	
[?? 	
MenuItem??	 
(?? 
$str?? 7
)??7 8
]??8 9
public@@ 
static@@ 
void@@ 1
%CreateProjectContextInDefaultLocation@@ @
(@@@ A
)@@A B
{AA 	
varBB 
fullDirPathBB 
=BB 
PathBB "
.BB" #
CombineBB# *
(BB* +
ApplicationBB+ 6
.BB6 7
dataPathBB7 ?
,BB? @
$strBBA L
)BBL M
;BBM N
ifDD 
(DD 
!DD 
	DirectoryDD 
.DD 
ExistsDD !
(DD! "
fullDirPathDD" -
)DD- .
)DD. /
{EE 
	DirectoryFF 
.FF 
CreateDirectoryFF )
(FF) *
fullDirPathFF* 5
)FF5 6
;FF6 7
}GG (
CreateProjectContextInternalII (
(II( )
$strII) ;
)II; <
;II< =
}JJ 	
[LL 	
MenuItemLL	 
(LL 
$strLL G
,LLG H
falseLLI N
,LLN O
$numLLP R
)LLR S
]LLS T
publicMM 
staticMM 
voidMM ,
 CreateDefaultSceneContractConfigMM ;
(MM; <
)MM< =
{NN 	
varOO 

folderPathOO 
=OO 
ZenUnityEditorUtilOO /
.OO/ 05
)GetCurrentDirectoryAssetPathFromSelectionOO0 Y
(OOY Z
)OOZ [
;OO[ \
ifQQ 
(QQ 
!QQ 

folderPathQQ 
.QQ 
EndsWithQQ $
(QQ$ %
$strQQ% 1
)QQ1 2
)QQ2 3
{RR 
EditorUtilitySS 
.SS 
DisplayDialogSS +
(SS+ ,
$strSS, 3
,SS3 4
$str	TT ë
,
TTë í
$str
TTì ó
)
TTó ò
;
TTò ô
returnUU 
;UU 
}VV 
varXX 
configXX 
=XX 
ScriptableObjectXX )
.XX) *
CreateInstanceXX* 8
<XX8 9&
DefaultSceneContractConfigXX9 S
>XXS T
(XXT U
)XXU V
;XXV W
ZenUnityEditorUtilZZ 
.ZZ %
SaveScriptableObjectAssetZZ 8
(ZZ8 9
Path[[ 
.[[ 
Combine[[ 
([[ 

folderPath[[ '
,[[' (&
DefaultSceneContractConfig[[) C
.[[C D
ResourcePath[[D P
+[[Q R
$str[[S [
)[[[ \
,[[\ ]
config[[^ d
)[[d e
;[[e f
}\\ 	
[^^ 	
MenuItem^^	 
(^^ 
$str^^ E
,^^E F
false^^G L
,^^L M
$num^^N O
)^^O P
]^^P Q
public__ 
static__ 
void__ +
CreateScriptableObjectInstaller__ :
(__: ;
)__; <
{`` 	"
AddCSharpClassTemplateaa "
(aa" #
$straa# @
,aa@ A
$straaB U
,aaU V
$strbb &
+cc 
$strcc $
+dd 
$strdd 
+ee 
$stree h
+ff 
$strff U
+gg 
$strgg 
+hh 
$strhh @
+ii 
$strii 
+jj 
$strjj 
+kk 
$strkk 
)kk 
;kk 
}ll 	
[nn 	
MenuItemnn	 
(nn 
$strnn 8
,nn8 9
falsenn: ?
,nn? @
$numnnA B
)nnB C
]nnC D
publicoo 
staticoo 
voidoo 
CreateMonoInstalleroo .
(oo. /
)oo/ 0
{pp 	"
AddCSharpClassTemplateqq "
(qq" #
$strqq# 3
,qq3 4
$strqq5 H
,qqH I
$strrr &
+ss 
$strss $
+tt 
$strtt 
+uu 
$struu =
+vv 
$strvv 
+ww 
$strww @
+xx 
$strxx 
+yy 
$stryy 
+zz 
$strzz 
)zz 
;zz 
}{{ 	
[}} 	
MenuItem}}	 
(}} 
$str}} 3
,}}3 4
false}}5 :
,}}: ;
$num}}< =
)}}= >
]}}> ?
public~~ 
static~~ 
void~~ 
CreateInstaller~~ *
(~~* +
)~~+ ,
{ 	$
AddCSharpClassTemplate
ÄÄ "
(
ÄÄ" #
$str
ÄÄ# .
,
ÄÄ. /
$str
ÄÄ0 C
,
ÄÄC D
$str
ÅÅ &
+
ÇÇ 
$str
ÇÇ $
+
ÉÉ 
$str
ÉÉ 
+
ÑÑ 
$str
ÑÑ E
+
ÖÖ 
$str
ÖÖ 
+
ÜÜ 
$str
ÜÜ @
+
áá 
$str
áá 
+
àà 
$str
àà 
+
ââ 
$str
ââ 
)
ââ 
;
ââ 
}
ää 	
[
åå 	
MenuItem
åå	 
(
åå 
$str
åå 7
,
åå7 8
false
åå9 >
,
åå> ?
$num
åå@ B
)
ååB C
]
ååC D
public
çç 
static
çç 
void
çç  
CreateEditorWindow
çç -
(
çç- .
)
çç. /
{
éé 	$
AddCSharpClassTemplate
èè "
(
èè" #
$str
èè# 2
,
èè2 3
$str
èè4 J
,
èèJ K
$str
êê &
+
ëë 
$str
ëë (
+
íí 
$str
íí $
+
ìì 
$str
ìì 
+
îî 
$str
îî C
+
ïï 
$str
ïï 
+
ññ 
$str
ññ ;
+
óó 
$str
óó F
+
òò 
$str
òò 
+
ôô 
$str
ôô P
+
öö 
$str
öö S
+
õõ 
$str
õõ ,
+
úú 
$str
úú 
+
ùù 
$str
ùù 
+
ûû 
$str
ûû @
+
üü 
$str
üü 
+
†† 
$str
†† %
+
°° 
$str
°° 
+
¢¢ 
$str
¢¢ 
)
¢¢ 
;
¢¢ 
}
££ 	
[
•• 	
MenuItem
••	 
(
•• 
$str
•• 9
,
••9 :
false
••; @
,
••@ A
$num
••B D
)
••D E
]
••E F
public
¶¶ 
static
¶¶ 
void
¶¶ "
CreateProjectContext
¶¶ /
(
¶¶/ 0
)
¶¶0 1
{
ßß 	
var
®® 
absoluteDir
®® 
=
®®  
ZenUnityEditorUtil
®® 0
.
®®0 13
%TryGetSelectedFolderPathInProjectsTab
®®1 V
(
®®V W
)
®®W X
;
®®X Y
if
™™ 
(
™™ 
absoluteDir
™™ 
==
™™ 
null
™™ #
)
™™# $
{
´´ 
EditorUtility
¨¨ 
.
¨¨ 
DisplayDialog
¨¨ +
(
¨¨+ ,
$str
¨¨, 3
,
¨¨3 4
$str≠≠ £
.
ÆÆ 
Fmt
ÆÆ 
(
ÆÆ 
ProjectContext
ÆÆ '
.
ÆÆ' ((
ProjectContextResourcePath
ÆÆ( B
)
ÆÆB C
,
ÆÆC D
$str
ÆÆE I
)
ÆÆI J
;
ÆÆJ K
return
ØØ 
;
ØØ 
}
∞∞ 
var
≤≤ 
parentFolderName
≤≤  
=
≤≤! "
Path
≤≤# '
.
≤≤' (
GetFileName
≤≤( 3
(
≤≤3 4
absoluteDir
≤≤4 ?
)
≤≤? @
;
≤≤@ A
if
¥¥ 
(
¥¥ 
parentFolderName
¥¥  
!=
¥¥! #
$str
¥¥$ /
)
¥¥/ 0
{
µµ 
EditorUtility
∂∂ 
.
∂∂ 
DisplayDialog
∂∂ +
(
∂∂+ ,
$str
∂∂, 3
,
∂∂3 4
$str∑∑ ∞
.
∏∏ 
Fmt
∏∏ 
(
∏∏ 
ProjectContext
∏∏ '
.
∏∏' ((
ProjectContextResourcePath
∏∏( B
)
∏∏B C
,
∏∏C D
$str
∏∏E I
)
∏∏I J
;
∏∏J K
return
ππ 
;
ππ 
}
∫∫ *
CreateProjectContextInternal
ºº (
(
ºº( )
absoluteDir
ºº) 4
)
ºº4 5
;
ºº5 6
}
ΩΩ 	
static
øø 
void
øø *
CreateProjectContextInternal
øø 0
(
øø0 1
string
øø1 7
absoluteDir
øø8 C
)
øøC D
{
¿¿ 	
var
¡¡ 
	assetPath
¡¡ 
=
¡¡  
ZenUnityEditorUtil
¡¡ .
.
¡¡. /0
"ConvertFullAbsolutePathToAssetPath
¡¡/ Q
(
¡¡Q R
absoluteDir
¡¡R ]
)
¡¡] ^
;
¡¡^ _
var
¬¬ 

prefabPath
¬¬ 
=
¬¬ 
(
¬¬ 
Path
¬¬ "
.
¬¬" #
Combine
¬¬# *
(
¬¬* +
	assetPath
¬¬+ 4
,
¬¬4 5
ProjectContext
¬¬6 D
.
¬¬D E(
ProjectContextResourcePath
¬¬E _
)
¬¬_ `
+
¬¬a b
$str
¬¬c l
)
¬¬l m
.
¬¬m n
Replace
¬¬n u
(
¬¬u v
$str
¬¬v z
,
¬¬z {
$str
¬¬| 
)¬¬ Ä
;¬¬Ä Å
var
ƒƒ 

gameObject
ƒƒ 
=
ƒƒ 
new
ƒƒ  

GameObject
ƒƒ! +
(
ƒƒ+ ,
)
ƒƒ, -
;
ƒƒ- .
try
∆∆ 
{
«« 

gameObject
»» 
.
»» 
AddComponent
»» '
<
»»' (
ProjectContext
»»( 6
>
»»6 7
(
»»7 8
)
»»8 9
;
»»9 :
var
ÀÀ 
	prefabObj
ÀÀ 
=
ÀÀ 
PrefabUtility
ÀÀ  -
.
ÀÀ- .
SaveAsPrefabAsset
ÀÀ. ?
(
ÀÀ? @

gameObject
ÀÀ@ J
,
ÀÀJ K

prefabPath
ÀÀL V
)
ÀÀV W
;
ÀÀW X
	Selection
–– 
.
–– 
activeObject
–– &
=
––' (
	prefabObj
––) 2
;
––2 3
}
—— 
finally
““ 
{
”” 

GameObject
‘‘ 
.
‘‘ 
DestroyImmediate
‘‘ +
(
‘‘+ ,

gameObject
‘‘, 6
)
‘‘6 7
;
‘‘7 8
}
’’ 
Debug
◊◊ 
.
◊◊ 
Log
◊◊ 
(
◊◊ 
$str
◊◊ ;
.
◊◊; <
Fmt
◊◊< ?
(
◊◊? @

prefabPath
◊◊@ J
)
◊◊J K
)
◊◊K L
;
◊◊L M
}
ÿÿ 	
public
⁄⁄ 
static
⁄⁄ 
string
⁄⁄ $
AddCSharpClassTemplate
⁄⁄ 3
(
⁄⁄3 4
string
€€ 
friendlyName
€€ 
,
€€  
string
€€! '
defaultFileName
€€( 7
,
€€7 8
string
€€9 ?
templateStr
€€@ K
)
€€K L
{
‹‹ 	
return
›› $
AddCSharpClassTemplate
›› )
(
››) *
friendlyName
ﬁﬁ 
,
ﬁﬁ 
defaultFileName
ﬁﬁ -
,
ﬁﬁ- .
templateStr
ﬁﬁ/ :
,
ﬁﬁ: ; 
ZenUnityEditorUtil
ﬁﬁ< N
.
ﬁﬁN O7
)GetCurrentDirectoryAssetPathFromSelection
ﬁﬁO x
(
ﬁﬁx y
)
ﬁﬁy z
)
ﬁﬁz {
;
ﬁﬁ{ |
}
ﬂﬂ 	
public
·· 
static
·· 
string
·· $
AddCSharpClassTemplate
·· 3
(
··3 4
string
‚‚ 
friendlyName
‚‚ 
,
‚‚  
string
‚‚! '
defaultFileName
‚‚( 7
,
‚‚7 8
string
„„ 
templateStr
„„ 
,
„„ 
string
„„  &

folderPath
„„' 1
)
„„1 2
{
‰‰ 	
var
ÂÂ 
absolutePath
ÂÂ 
=
ÂÂ 
EditorUtility
ÂÂ ,
.
ÂÂ, -
SaveFilePanel
ÂÂ- :
(
ÂÂ: ;
$str
ÊÊ "
+
ÊÊ# $
friendlyName
ÊÊ% 1
,
ÊÊ1 2

folderPath
ÁÁ 
,
ÁÁ 
defaultFileName
ËË 
+
ËË  !
$str
ËË" '
,
ËË' (
$str
ÈÈ 
)
ÈÈ 
;
ÈÈ 
if
ÎÎ 
(
ÎÎ 
absolutePath
ÎÎ 
==
ÎÎ 
$str
ÎÎ  "
)
ÎÎ" #
{
ÏÏ 
return
ÓÓ 
null
ÓÓ 
;
ÓÓ 
}
ÔÔ 
if
ÒÒ 
(
ÒÒ 
!
ÒÒ 
absolutePath
ÒÒ 
.
ÒÒ 
ToLower
ÒÒ %
(
ÒÒ% &
)
ÒÒ& '
.
ÒÒ' (
EndsWith
ÒÒ( 0
(
ÒÒ0 1
$str
ÒÒ1 6
)
ÒÒ6 7
)
ÒÒ7 8
{
ÚÚ 
absolutePath
ÛÛ 
+=
ÛÛ 
$str
ÛÛ  %
;
ÛÛ% &
}
ÙÙ 
var
ˆˆ 
	className
ˆˆ 
=
ˆˆ 
Path
ˆˆ  
.
ˆˆ  !)
GetFileNameWithoutExtension
ˆˆ! <
(
ˆˆ< =
absolutePath
ˆˆ= I
)
ˆˆI J
;
ˆˆJ K
File
˜˜ 
.
˜˜ 
WriteAllText
˜˜ 
(
˜˜ 
absolutePath
˜˜ *
,
˜˜* +
templateStr
˜˜, 7
.
˜˜7 8
Replace
˜˜8 ?
(
˜˜? @
$str
˜˜@ L
,
˜˜L M
	className
˜˜N W
)
˜˜W X
)
˜˜X Y
;
˜˜Y Z
AssetDatabase
˘˘ 
.
˘˘ 
Refresh
˘˘ !
(
˘˘! "
)
˘˘" #
;
˘˘# $
var
˚˚ 
	assetPath
˚˚ 
=
˚˚  
ZenUnityEditorUtil
˚˚ .
.
˚˚. /0
"ConvertFullAbsolutePathToAssetPath
˚˚/ Q
(
˚˚Q R
absolutePath
˚˚R ^
)
˚˚^ _
;
˚˚_ `
EditorUtility
˝˝ 
.
˝˝  
FocusProjectWindow
˝˝ ,
(
˝˝, -
)
˝˝- .
;
˝˝. /
	Selection
˛˛ 
.
˛˛ 
activeObject
˛˛ "
=
˛˛# $
AssetDatabase
˛˛% 2
.
˛˛2 3
LoadAssetAtPath
˛˛3 B
<
˛˛B C
UnityEngine
˛˛C N
.
˛˛N O
Object
˛˛O U
>
˛˛U V
(
˛˛V W
	assetPath
˛˛W `
)
˛˛` a
;
˛˛a b
return
ÄÄ 
	assetPath
ÄÄ 
;
ÄÄ 
}
ÅÅ 	
[
ÉÉ 	
MenuItem
ÉÉ	 
(
ÉÉ 
$str
ÉÉ ;
)
ÉÉ; <
]
ÉÉ< =
public
ÑÑ 
static
ÑÑ 
void
ÑÑ %
ValidateAllActiveScenes
ÑÑ 2
(
ÑÑ2 3
)
ÑÑ3 4
{
ÖÖ 	 
ZenUnityEditorUtil
ÜÜ 
.
ÜÜ +
SaveThenRunPreserveSceneSetup
ÜÜ <
(
ÜÜ< =
(
ÜÜ= >
)
ÜÜ> ?
=>
ÜÜ@ B
{
áá 
var
àà 
numValidated
àà $
=
àà% & 
ZenUnityEditorUtil
àà' 9
.
àà9 :%
ValidateAllActiveScenes
àà: Q
(
ààQ R
)
ààR S
;
ààS T
Log
ââ 
.
ââ 
Info
ââ 
(
ââ 
$str
ââ M
,
ââM N
numValidated
ââO [
)
ââ[ \
;
ââ\ ]
}
ää 
)
ää 
;
ää 
}
ãã 	
static
çç 
bool
çç *
ValidateCurrentSceneInternal
çç 0
(
çç0 1
)
çç1 2
{
éé 	
return
èè  
ZenUnityEditorUtil
èè %
.
èè% &+
SaveThenRunPreserveSceneSetup
èè& C
(
èèC D
(
èèD E
)
èèE F
=>
èèG I
{
êê (
SceneParentAutomaticLoader
ëë .
.
ëë. /?
1ValidateMultiSceneSetupAndLoadDefaultSceneParents
ëë/ `
(
ëë` a
)
ëëa b
;
ëëb c 
ZenUnityEditorUtil
íí &
.
íí& ''
ValidateCurrentSceneSetup
íí' @
(
íí@ A
)
ííA B
;
ííB C
Log
ìì 
.
ìì 
Info
ìì 
(
ìì 
$str
ìì @
)
ìì@ A
;
ììA B
}
îî 
)
îî 
;
îî 
}
ïï 	
[
óó 	
MenuItem
óó	 
(
óó 
$str
óó 3
,
óó3 4
false
óó5 :
,
óó: ;
$num
óó< >
)
óó> ?
]
óó? @
public
òò 
static
òò 
void
òò 
CreateUnitTest
òò )
(
òò) *
)
òò* +
{
ôô 	$
AddCSharpClassTemplate
öö "
(
öö" #
$str
öö# .
,
öö. /
$str
öö0 B
,
ööB C
$str
õõ "
+
úú 
$str
úú ,
+
ùù 
$str
ùù 
+
ûû 
$str
ûû #
+
üü 
$str
üü F
+
†† 
$str
†† 
+
°° 
$str
°°  
+
¢¢ 
$str
¢¢ 0
+
££ 
$str
££ 
+
§§ 
$str
§§ %
+
•• 
$str
•• 
+
¶¶ 
$str
¶¶ 
)
¶¶ 
;
¶¶ 
}
ßß 	
[
©© 	
MenuItem
©©	 
(
©© 
$str
©© :
,
©©: ;
false
©©< A
,
©©A B
$num
©©C E
)
©©E F
]
©©F G
public
™™ 
static
™™ 
void
™™ #
CreateIntegrationTest
™™ 0
(
™™0 1
)
™™1 2
{
´´ 	$
AddCSharpClassTemplate
¨¨ "
(
¨¨" #
$str
¨¨# 5
,
¨¨5 6
$str
¨¨7 P
,
¨¨P Q
$str
≠≠ "
+
ÆÆ 
$str
ÆÆ /
+
ØØ 
$str
ØØ 2
+
∞∞ 
$str
∞∞ 
+
±± 
$str
±± M
+
≤≤ 
$str
≤≤ 
+
≥≥ 
$str
≥≥ %
+
¥¥ 
$str
¥¥ 7
+
µµ 
$str
µµ 
+
∂∂ 
$str
∂∂ w
+
∑∑ 
$str
∑∑ 
+
∏∏ 
$str
∏∏ +
+
ππ 
$str
ππ 
+
∫∫ 
$str
∫∫ <
+
ªª 
$str
ªª 
+
ºº 
$str
ºº ,
+
ΩΩ 
$str
ΩΩ 
+
ææ 
$str
ææ G
+
øø 
$str
øø K
+
¿¿ 
$str
¿¿ *
+
¡¡ 
$str
¡¡ 
+
¬¬ 
$str
¬¬ 
)
¬¬ 
;
¬¬ 
}
√√ 	
[
≈≈ 	
MenuItem
≈≈	 
(
≈≈ 
$str
≈≈ 4
,
≈≈4 5
false
≈≈6 ;
,
≈≈; <
$num
≈≈= ?
)
≈≈? @
]
≈≈@ A
public
∆∆ 
static
∆∆ 
void
∆∆ 
CreateSceneTest
∆∆ *
(
∆∆* +
)
∆∆+ ,
{
«« 	$
AddCSharpClassTemplate
»» "
(
»»" #
$str
»»# 7
,
»»7 8
$str
»»9 L
,
»»L M
$str
…… "
+
   
$str
   /
+
ÀÀ 
$str
ÀÀ (
+
ÃÃ 
$str
ÃÃ 2
+
ÕÕ 
$str
ÕÕ 
+
ŒŒ 
$str
ŒŒ @
+
œœ 
$str
œœ 
+
–– 
$str
–– %
+
—— 
$str
—— 8
+
““ 
$str
““ 
+
”” 
$str
”” N
+
‘‘ 
$str
‘‘ 
+
’’ 
$str
’’ Y
+
÷÷ 
$str
÷÷ 
+
◊◊ 
$str
◊◊ F
+
ÿÿ 
$str
ÿÿ 
+
ŸŸ 
$str
ŸŸ }
+
⁄⁄ 
$str
⁄⁄ 
+
€€ 
$str
€€ 
)
€€ 
;
€€ 
}
‹‹ 	
}
›› 
}ﬁﬁ æ
fC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\ProjectContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( 
ProjectContext '
)' (
)( )
]) *
[ 
NoReflectionBaking 
] 
public		 

class		  
ProjectContextEditor		 %
:		& '
ContextEditor		( 5
{

 
SerializedProperty 
_settingsProperty ,
;, -
SerializedProperty 7
+_editorReflectionBakingCoverageModeProperty F
;F G
SerializedProperty 7
+_buildsReflectionBakingCoverageModeProperty F
;F G
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; 
_settingsProperty 
= 
serializedObject  0
.0 1
FindProperty1 =
(= >
$str> I
)I J
;J K7
+_editorReflectionBakingCoverageModeProperty 7
=8 9
serializedObject: J
.J K
FindPropertyK W
(W X
$strX }
)} ~
;~ 7
+_buildsReflectionBakingCoverageModeProperty 7
=8 9
serializedObject: J
.J K
FindPropertyK W
(W X
$strX }
)} ~
;~ 
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *
_settingsProperty* ;
,; <
true= A
)A B
;B C
EditorGUILayout 
. 
PropertyField )
() *7
+_editorReflectionBakingCoverageModeProperty* U
,U V
trueW [
)[ \
;\ ]
EditorGUILayout 
. 
PropertyField )
() *7
+_buildsReflectionBakingCoverageModeProperty* U
,U V
trueW [
)[ \
;\ ]
} 	
}   
}!! ¬
wC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\SceneParentLoading\DefaultSceneContractConfig.cs
	namespace 	
Zenject
 
. 
Internal 
{ 
public 

class &
DefaultSceneContractConfig +
:, -
ScriptableObject. >
{		 
public

 
const

 
string

 
ResourcePath

 (
=

) *
$str

+ N
;

N O
public 
List 
< 
ContractInfo  
>  !
DefaultContracts" 2
;2 3
[ 	
Serializable	 
] 
public 
class 
ContractInfo !
{ 	
public 
string 
ContractName &
;& '
public 

SceneAsset 
Scene #
;# $
} 	
} 
} é
mC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\SceneDecoratorContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
CustomEditor 
( 
typeof 
( !
SceneDecoratorContext .
). /
)/ 0
]0 1
[ 
NoReflectionBaking 
] 
public 

class '
SceneDecoratorContextEditor ,
:- .
ContextEditor/ <
{ 
SerializedProperty *
_decoratedContractNameProperty 9
;9 :
	protected 
override 
string !
[! "
]" #
PropertyNames$ 1
{ 	
get 
{ 
return 
base 
. 
PropertyNames )
.) *
Concat* 0
(0 1
new1 4
string5 ;
[; <
]< =
{ 
$str )
,) *
$str /
,/ 0
$str 9
} 
) 
. 
ToArray 
( 
) 
; 
}   
}!! 	
	protected## 
override## 
string## !
[##! "
]##" # 
PropertyDisplayNames##$ 8
{$$ 	
get%% 
{&& 
return'' 
base'' 
.''  
PropertyDisplayNames'' 0
.''0 1
Concat''1 7
(''7 8
new''8 ;
string''< B
[''B C
]''C D
{(( 
$str)) )
,))) *
$str** 0
,**0 1
$str++ ;
},, 
),, 
.-- 
ToArray-- 
(-- 
)-- 
;-- 
}.. 
}// 	
	protected11 
override11 
string11 !
[11! "
]11" # 
PropertyDescriptions11$ 8
{22 	
get33 
{44 
return55 
base55 
.55  
PropertyDescriptions55 0
.550 1
Concat551 7
(557 8
new558 ;
string55< B
[55B C
]55C D
{66 
$str	77 ü
,
77ü †
$str	88 ë
,
88ë í
$str	99 §
}:: 
):: 
.;; 
ToArray;; 
(;; 
);; 
;;; 
}<< 
}== 	
public?? 
override?? 
void?? 
OnEnable?? %
(??% &
)??& '
{@@ 	
baseAA 
.AA 
OnEnableAA 
(AA 
)AA 
;AA *
_decoratedContractNamePropertyCC *
=CC+ ,
serializedObjectCC- =
.CC= >
FindPropertyCC> J
(CCJ K
$strCCK c
)CCc d
;CCd e
}DD 	
	protectedFF 
overrideFF 
voidFF 
OnGuiFF  %
(FF% &
)FF& '
{GG 	
baseHH 
.HH 
OnGuiHH 
(HH 
)HH 
;HH 
EditorGUILayoutJJ 
.JJ 
PropertyFieldJJ )
(JJ) **
_decoratedContractNamePropertyJJ* H
)JJH I
;JJI J
}KK 	
}LL 
}MM Û@
_C:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\ObjectGraphVisualizer.cs
	namespace 	
Zenject
 
{ 
public 

static 
class !
ObjectGraphVisualizer -
{ 
public 
static 
void #
OutputObjectGraphToFile 2
(2 3
DiContainer 
	container !
,! "
string# )

outputPath* 4
,4 5
IEnumerable 
< 
Type 
> 
externalIgnoreTypes 1
,1 2
IEnumerable3 >
<> ?
Type? C
>C D
contractTypesE R
)R S
{ 	
var 
graph 
=  
CalculateObjectGraph ,
(, -
	container- 6
,6 7
contractTypes8 E
)E F
;F G
var 
ignoreTypes 
= 
new !
List" &
<& '
Type' +
>+ ,
{ 
typeof 
( 
DiContainer "
)" #
,# $
typeof 
(  
InitializableManager +
)+ ,
} 
; 
ignoreTypes 
. 
AddRange  
(  !
externalIgnoreTypes! 4
)4 5
;5 6
var 
	resultStr 
= 
$str *
;* +
	resultStr   
+=   
$str   (
;  ( )
foreach"" 
("" 
var"" 
entry"" 
in"" !
graph""" '
)""' (
{## 
if$$ 
($$ 
ShouldIgnoreType$$ $
($$$ %
entry$$% *
.$$* +
Key$$+ .
,$$. /
ignoreTypes$$0 ;
)$$; <
)$$< =
{%% 
continue&& 
;&& 
}'' 
foreach)) 
()) 
var)) 
dependencyType)) +
in)), .
entry))/ 4
.))4 5
Value))5 :
))): ;
{** 
if++ 
(++ 
ShouldIgnoreType++ (
(++( )
dependencyType++) 7
,++7 8
ignoreTypes++9 D
)++D E
)++E F
{,, 
continue--  
;--  !
}.. 
	resultStr00 
+=00   
GetFormattedTypeName00! 5
(005 6
entry006 ;
.00; <
Key00< ?
)00? @
+00A B
$str00C I
+00J K 
GetFormattedTypeName00L `
(00` a
dependencyType00a o
)00o p
+00q r
$str00s y
;00y z
}11 
}22 
	resultStr44 
+=44 
$str44 
;44 
File66 
.66 
WriteAllText66 
(66 

outputPath66 (
,66( )
	resultStr66* 3
)663 4
;664 5
}77 	
static99 
bool99 
ShouldIgnoreType99 $
(99$ %
Type99% )
type99* .
,99. /
List990 4
<994 5
Type995 9
>999 :
ignoreTypes99; F
)99F G
{:: 	
return;; 
ignoreTypes;; 
.;; 
Contains;; '
(;;' (
type;;( ,
);;, -
;;;- .
}<< 	
static>> 

Dictionary>> 
<>> 
Type>> 
,>> 
List>>  $
<>>$ %
Type>>% )
>>>) *
>>>* + 
CalculateObjectGraph>>, @
(>>@ A
DiContainer?? 
	container?? !
,??! "
IEnumerable??# .
<??. /
Type??/ 3
>??3 4
	contracts??5 >
)??> ?
{@@ 	
varAA 
mapAA 
=AA 
newAA 

DictionaryAA $
<AA$ %
TypeAA% )
,AA) *
ListAA+ /
<AA/ 0
TypeAA0 4
>AA4 5
>AA5 6
(AA6 7
)AA7 8
;AA8 9
foreachCC 
(CC 
varCC 
contractTypeCC %
inCC& (
	contractsCC) 2
)CC2 3
{DD 
varEE 
dependsEE 
=EE 
GetDependenciesEE -
(EE- .
	containerEE. 7
,EE7 8
contractTypeEE9 E
)EEE F
;EEF G
ifGG 
(GG 
dependsGG 
.GG 
AnyGG 
(GG  
)GG  !
)GG! "
{HH 
mapII 
.II 
AddII 
(II 
contractTypeII (
,II( )
dependsII* 1
)II1 2
;II2 3
}JJ 
}KK 
returnMM 
mapMM 
;MM 
}NN 	
staticPP 
ListPP 
<PP 
TypePP 
>PP 
GetDependenciesPP )
(PP) *
DiContainerQQ 
	containerQQ !
,QQ! "
TypeQQ# '
typeQQ( ,
)QQ, -
{RR 	
varSS 
dependenciesSS 
=SS 
newSS "
ListSS# '
<SS' (
TypeSS( ,
>SS, -
(SS- .
)SS. /
;SS/ 0
foreachUU 
(UU 
varUU 
contractTypeUU %
inUU& (
	containerUU) 2
.UU2 3"
GetDependencyContractsUU3 I
(UUI J
typeUUJ N
)UUN O
)UUO P
{VV 
ListWW 
<WW 
TypeWW 
>WW 
dependTypesWW &
;WW& '
ifYY 
(YY 
contractTypeYY  
.YY  !
FullNameYY! )
.YY) *

StartsWithYY* 4
(YY4 5
$strYY5 V
)YYV W
)YYW X
{ZZ 
var[[ 
subTypes[[  
=[[! "
contractType[[# /
.[[/ 0
GenericArguments[[0 @
([[@ A
)[[A B
;[[B C
Assert\\ 
.\\ 
IsEqual\\ "
(\\" #
subTypes\\# +
.\\+ ,
Length\\, 2
,\\2 3
$num\\4 5
)\\5 6
;\\6 7
var^^ 
subType^^ 
=^^  !
subTypes^^" *
[^^* +
$num^^+ ,
]^^, -
;^^- .
dependTypes__ 
=__  !
	container__" +
.__+ ,
ResolveTypeAll__, :
(__: ;
subType__; B
)__B C
;__C D
}`` 
elseaa 
{bb 
dependTypescc 
=cc  !
	containercc" +
.cc+ ,
ResolveTypeAllcc, :
(cc: ;
contractTypecc; G
)ccG H
;ccH I
Assertdd 
.dd 
Thatdd 
(dd  
dependTypesdd  +
.dd+ ,
Countdd, 1
<=dd2 4
$numdd5 6
)dd6 7
;dd7 8
}ee 
foreachgg 
(gg 
vargg 

dependTypegg '
ingg( *
dependTypesgg+ 6
)gg6 7
{hh 
dependenciesii  
.ii  !
Addii! $
(ii$ %

dependTypeii% /
)ii/ 0
;ii0 1
}jj 
}kk 
returnmm 
dependenciesmm 
;mm  
}nn 	
staticpp 
stringpp  
GetFormattedTypeNamepp *
(pp* +
Typepp+ /
typepp0 4
)pp4 5
{qq 	
varrr 
strrr 
=rr 
typerr 
.rr 

PrettyNamerr %
(rr% &
)rr& '
;rr' (
struu 
=uu 
struu 
.uu 
Replaceuu 
(uu 
$struu !
,uu! "
$struu# &
)uu& '
;uu' (
strvv 
=vv 
strvv 
.vv 
Replacevv 
(vv 
$strvv !
,vv! "
$strvv# &
)vv& '
;vv' (
strww 
=ww 
strww 
.ww 
Replaceww 
(ww 
$strww !
,ww! "
$strww# &
)ww& '
;ww' (
returnyy 
stryy 
;yy 
}zz 	
}{{ 
}|| ì
_C:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\EditorStaticInstaller.cs
	namespace 	
Zenject
 
{ 
public 

abstract 
class !
EditorStaticInstaller /
</ 0
T0 1
>1 2
:3 4
InstallerBase5 B
where 
T 
: !
EditorStaticInstaller '
<' (
T( )
>) *
{ 
public 
static 
void 
Install "
(" #
)# $
{ 	
StaticContext 
. 
	Container #
.# $
Instantiate$ /
</ 0
T0 1
>1 2
(2 3
)3 4
.4 5
InstallBindings5 D
(D E
)E F
;F G
} 	
} 
} À5
bC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\UnityInspectorListEditor.cs
	namespace 	
Zenject
 
{		 
public

 

abstract

 
class

 $
UnityInspectorListEditor

 2
:

3 4
Editor

5 ;
{ 
List 
< 
ReorderableList 
> 
_installersLists .
;. /
List 
< 
SerializedProperty 
>  !
_installersProperties! 6
;6 7
	protected 
abstract 
string !
[! "
]" # 
PropertyDisplayNames$ 8
{ 	
get 
; 
} 	
	protected 
abstract 
string !
[! "
]" #
PropertyNames$ 1
{ 	
get 
; 
} 	
	protected 
abstract 
string !
[! "
]" # 
PropertyDescriptions$ 8
{ 	
get 
; 
} 	
public 
virtual 
void 
OnEnable $
($ %
)% &
{ 	!
_installersProperties   !
=  " #
new  $ '
List  ( ,
<  , -
SerializedProperty  - ?
>  ? @
(  @ A
)  A B
;  B C
_installersLists!! 
=!! 
new!! "
List!!# '
<!!' (
ReorderableList!!( 7
>!!7 8
(!!8 9
)!!9 :
;!!: ;
var## 
descriptions## 
=##  
PropertyDescriptions## 3
;##3 4
var$$ 
names$$ 
=$$ 
PropertyNames$$ %
;$$% &
var%% 
displayNames%% 
=%%  
PropertyDisplayNames%% 3
;%%3 4
Assert'' 
.'' 
IsEqual'' 
('' 
descriptions'' '
.''' (
Length''( .
,''. /
names''0 5
.''5 6
Length''6 <
)''< =
;''= >
var)) 
infos)) 
=)) 

Enumerable)) "
.))" #
Range))# (
())( )
$num))) *
,))* +
names)), 1
.))1 2
Length))2 8
)))8 9
.))9 :
Select)): @
())@ A
i))A B
=>))C E
new))F I
{))J K
Name))L P
=))Q R
names))S X
[))X Y
i))Y Z
]))Z [
,))[ \
DisplayName))] h
=))i j
displayNames))k w
[))w x
i))x y
]))y z
,))z {
Description	))| á
=
))à â
descriptions
))ä ñ
[
))ñ ó
i
))ó ò
]
))ò ô
}
))ö õ
)
))õ ú
.
))ú ù
ToList
))ù £
(
))£ §
)
))§ •
;
))• ¶
foreach++ 
(++ 
var++ 
info++ 
in++  
infos++! &
)++& '
{,, 
var-- 
installersProperty-- &
=--' (
serializedObject--) 9
.--9 :
FindProperty--: F
(--F G
info--G K
.--K L
Name--L P
)--P Q
;--Q R!
_installersProperties.. %
...% &
Add..& )
(..) *
installersProperty..* <
)..< =
;..= >
ReorderableList00 
installersList00  .
=00/ 0
new001 4
ReorderableList005 D
(00D E
serializedObject00E U
,00U V
installersProperty00W i
,00i j
true00k o
,00o p
true00q u
,00u v
true00w {
,00{ |
true	00} Å
)
00Å Ç
;
00Ç É
_installersLists11  
.11  !
Add11! $
(11$ %
installersList11% 3
)113 4
;114 5
var33 

closedName33 
=33  
info33! %
.33% &
DisplayName33& 1
;331 2
var44 

closedDesc44 
=44  
info44! %
.44% &
Description44& 1
;441 2
installersList66 
.66 
drawHeaderCallback66 1
+=662 4
rect665 9
=>66: <
{77 
GUI88 
.88 
Label88 
(88 
rect88 "
,88" #
new99 

GUIContent99 "
(99" #

closedName99# -
,99- .

closedDesc99/ 9
)999 :
)99: ;
;99; <
}:: 
;:: 
installersList;; 
.;; 
drawElementCallback;; 2
+=;;3 5
(;;6 7
rect;;7 ;
,;;; <
index;;= B
,;;B C
active;;D J
,;;J K
focused;;L S
);;S T
=>;;U W
{<< 
rect== 
.== 
width== 
-=== !
$num==" $
;==$ %
rect>> 
.>> 
x>> 
+=>> 
$num>>  
;>>  !
	EditorGUI?? 
.?? 
PropertyField?? +
(??+ ,
rect??, 0
,??0 1
installersProperty??2 D
.??D E"
GetArrayElementAtIndex??E [
(??[ \
index??\ a
)??a b
,??b c

GUIContent??d n
.??n o
none??o s
,??s t
true??u y
)??y z
;??z {
}@@ 
;@@ 
}AA 
}BB 	
publicDD 
sealedDD 
overrideDD 
voidDD #
OnInspectorGUIDD$ 2
(DD2 3
)DD3 4
{EE 	
serializedObjectFF 
.FF 
UpdateFF #
(FF# $
)FF$ %
;FF% &
OnGuiHH 
(HH 
)HH 
;HH 
serializedObjectJJ 
.JJ #
ApplyModifiedPropertiesJJ 4
(JJ4 5
)JJ5 6
;JJ6 7
}KK 	
	protectedMM 
virtualMM 
voidMM 
OnGuiMM $
(MM$ %
)MM% &
{NN 	
ifOO 
(OO 
ApplicationOO 
.OO 
	isPlayingOO %
)OO% &
{PP 
GUIQQ 
.QQ 
enabledQQ 
=QQ 
falseQQ #
;QQ# $
}RR 
foreachTT 
(TT 
varTT 
listTT 
inTT  
_installersListsTT! 1
)TT1 2
{UU 
listVV 
.VV 
DoLayoutListVV !
(VV! "
)VV" #
;VV# $
}WW 
GUIYY 
.YY 
enabledYY 
=YY 
trueYY 
;YY 
}ZZ 	
}[[ 
}\\ »K
jC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\EditorWindow\ZenjectEditorWindow.cs
	namespace 	
Zenject
 
{ 
public 

abstract 
class 
ZenjectEditorWindow -
:. /
EditorWindow0 <
{		 
[

 	
Inject

	 
]

 
[ 	
NonSerialized	 
] 
Kernel 
_kernel 
; 
[ 	
Inject	 
] 
[ 	
NonSerialized	 
]  
GuiRenderableManager !
_guiRenderableManager 2
;2 3
[ 	
NonSerialized	 
] 
DiContainer 

_container 
; 
[ 	
NonSerialized	 
] 
	Exception 
_fatalError 
; 
[ 	
NonSerialized	 
] 
GUIStyle 
_errorTextStyle  
;  !
GUIStyle 
ErrorTextStyle 
{ 	
get 
{ 
if 
( 
_errorTextStyle #
==$ &
null' +
)+ ,
{   
_errorTextStyle!! #
=!!$ %
new!!& )
GUIStyle!!* 2
(!!2 3
GUI!!3 6
.!!6 7
skin!!7 ;
.!!; <
label!!< A
)!!A B
;!!B C
_errorTextStyle"" #
.""# $
fontSize""$ ,
=""- .
$num""/ 1
;""1 2
_errorTextStyle## #
.### $
normal##$ *
.##* +
	textColor##+ 4
=##5 6
Color##7 <
.##< =
red##= @
;##@ A
_errorTextStyle$$ #
.$$# $
wordWrap$$$ ,
=$$- .
true$$/ 3
;$$3 4
_errorTextStyle%% #
.%%# $
	alignment%%$ -
=%%. /

TextAnchor%%0 :
.%%: ;
MiddleCenter%%; G
;%%G H
}&& 
return(( 
_errorTextStyle(( &
;((& '
})) 
}** 	
	protected,, 
DiContainer,, 
	Container,, '
{-- 	
get.. 
{.. 
return.. 

_container.. #
;..# $
}..% &
}// 	
public11 
virtual11 
void11 
OnEnable11 $
(11$ %
)11% &
{22 	
if33 
(33 
_fatalError33 
!=33 
null33 #
)33# $
{44 
return55 
;55 
}66 

Initialize88 
(88 
)88 
;88 
}99 	
	protected;; 
virtual;; 
void;; 

Initialize;; )
(;;) *
);;* +
{<< 	
Assert== 
.== 
IsNull== 
(== 

_container== $
)==$ %
;==% &

_container?? 
=?? 
new?? 
DiContainer?? (
(??( )
new??) ,
[??, -
]??- .
{??/ 0
StaticContext??1 >
.??> ?
	Container??? H
}??I J
)??J K
;??K L

_containerBB 
.BB "
AssertOnNewGameObjectsBB -
=BB. /
trueBB0 4
;BB4 5$
ZenjectManagersInstallerDD $
.DD$ %
InstallDD% ,
(DD, -

_containerDD- 7
)DD7 8
;DD8 9

_containerFF 
.FF 
BindFF 
<FF 
KernelFF "
>FF" #
(FF# $
)FF$ %
.FF% &
AsSingleFF& .
(FF. /
)FF/ 0
;FF0 1

_containerGG 
.GG 
BindGG 
<GG  
GuiRenderableManagerGG 0
>GG0 1
(GG1 2
)GG2 3
.GG3 4
AsSingleGG4 <
(GG< =
)GG= >
;GG> ?

_containerHH 
.HH 
BindInstanceHH #
(HH# $
thisHH$ (
)HH( )
;HH) *
InstallBindingsJJ 
(JJ 
)JJ 
;JJ 

_containerLL 
.LL 
QueueForInjectLL %
(LL% &
thisLL& *
)LL* +
;LL+ ,

_containerMM 
.MM 
ResolveRootsMM #
(MM# $
)MM$ %
;MM% &
_kernelOO 
.OO 

InitializeOO 
(OO 
)OO  
;OO  !
}PP 	
publicRR 
virtualRR 
voidRR 
	OnDisableRR %
(RR% &
)RR& '
{SS 	
ifTT 
(TT 
_fatalErrorTT 
!=TT 
nullTT #
)TT# $
{UU 
returnVV 
;VV 
}WW 
_kernelYY 
.YY 
DisposeYY 
(YY 
)YY 
;YY 
}ZZ 	
public\\ 
virtual\\ 
void\\ 
Update\\ "
(\\" #
)\\# $
{]] 	
if^^ 
(^^ 
_fatalError^^ 
!=^^ 
null^^ #
)^^# $
{__ 
return`` 
;`` 
}aa 
trycc 
{dd 
_kernelee 
.ee 
Tickee 
(ee 
)ee 
;ee 
}ff 
catchgg 
(gg 
	Exceptiongg 
egg 
)gg 
{hh 
Logii 
.ii 
ErrorExceptionii "
(ii" #
eii# $
)ii$ %
;ii% &
_fatalErrorjj 
=jj 
ejj 
;jj  
}kk 
Repaintnn 
(nn 
)nn 
;nn 
}oo 	
publicqq 
virtualqq 
voidqq 
OnGUIqq !
(qq! "
)qq" #
{rr 	
ifss 
(ss 
_fatalErrorss 
!=ss 
nullss #
)ss# $
{tt 
varuu 

labelWidthuu 
=uu  
$numuu! $
;uu$ %
varvv 
labelHeightvv 
=vv  !
$numvv" %
;vv% &
GUIxx 
.xx 
Labelxx 
(xx 
newxx 
Rectxx "
(xx" #
Screenxx# )
.xx) *
widthxx* /
/xx0 1
$numxx2 3
-xx4 5

labelWidthxx6 @
/xxA B
$numxxC D
,xxD E
ScreenxxF L
.xxL M
heightxxM S
/xxT U
$numxxV W
-xxX Y
labelHeightxxZ e
/xxf g
$numxxh i
,xxi j

labelWidthxxk u
,xxu v
labelHeight	xxw Ç
)
xxÇ É
,
xxÉ Ñ
$str
xxÖ º
,
xxº Ω
ErrorTextStyle
xxæ Ã
)
xxÃ Õ
;
xxÕ Œ
varzz 
buttonWidthzz 
=zz  !
$numzz" %
;zz% &
var{{ 
buttonHeight{{  
={{! "
$num{{# %
;{{% &
var|| 
offset|| 
=|| 
new||  
Vector2||! (
(||( )
$num||) *
,||* +
$num||, /
)||/ 0
;||0 1
if~~ 
(~~ 
GUI~~ 
.~~ 
Button~~ 
(~~ 
new~~ "
Rect~~# '
(~~' (
Screen~~( .
.~~. /
width~~/ 4
/~~5 6
$num~~7 8
-~~9 :
buttonWidth~~; F
/~~G H
$num~~I J
+~~K L
offset~~M S
.~~S T
x~~T U
,~~U V
Screen~~W ]
.~~] ^
height~~^ d
/~~e f
$num~~g h
-~~i j
buttonHeight~~k w
/~~x y
$num~~z {
+~~| }
offset	~~~ Ñ
.
~~Ñ Ö
y
~~Ö Ü
,
~~Ü á
buttonWidth
~~à ì
,
~~ì î
buttonHeight
~~ï °
)
~~° ¢
,
~~¢ £
$str
~~§ ¨
)
~~¨ ≠
)
~~≠ Æ
{ 
ExecuteFullReload
ÄÄ %
(
ÄÄ% &
)
ÄÄ& '
;
ÄÄ' (
}
ÅÅ 
}
ÇÇ 
else
ÉÉ 
{
ÑÑ 
try
ÖÖ 
{
ÜÜ 
if
áá 
(
áá #
_guiRenderableManager
áá -
!=
áá. 0
null
áá1 5
)
áá5 6
{
àà #
_guiRenderableManager
ââ -
.
ââ- .
OnGui
ââ. 3
(
ââ3 4
)
ââ4 5
;
ââ5 6
}
ää 
}
ãã 
catch
åå 
(
åå 
	Exception
åå  
e
åå! "
)
åå" #
{
çç 
Log
éé 
.
éé 
ErrorException
éé &
(
éé& '
e
éé' (
)
éé( )
;
éé) *
_fatalError
èè 
=
èè  !
e
èè" #
;
èè# $
}
êê 
}
ëë 
}
íí 	
	protected
îî 
virtual
îî 
void
îî 
ExecuteFullReload
îî 0
(
îî0 1
)
îî1 2
{
ïï 	
_kernel
ññ 
=
ññ 
null
ññ 
;
ññ #
_guiRenderableManager
óó !
=
óó" #
null
óó$ (
;
óó( )

_container
òò 
=
òò 
null
òò 
;
òò 
_fatalError
ôô 
=
ôô 
null
ôô 
;
ôô 

Initialize
õõ 
(
õõ 
)
õõ 
;
õõ 
}
úú 	
public
ûû 
abstract
ûû 
void
ûû 
InstallBindings
ûû ,
(
ûû, -
)
ûû- .
;
ûû. /
}
üü 
}†† •
dC:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\SceneContextEditor.cs
	namespace 	
Zenject
 
{ 
[ "
CanEditMultipleObjects 
] 
[ 
CustomEditor 
( 
typeof 
( 
SceneContext %
)% &
)& '
]' (
[		 
NoReflectionBaking		 
]		 
public

 

class

 
SceneContextEditor

 #
:

$ %!
RunnableContextEditor

& ;
{ 
SerializedProperty !
_contractNameProperty 0
;0 1
SerializedProperty  
_parentNamesProperty /
;/ 0
SerializedProperty 6
*_parentNewObjectsUnderSceneContextProperty E
;E F
public 
override 
void 
OnEnable %
(% &
)& '
{ 	
base 
. 
OnEnable 
( 
) 
; !
_contractNameProperty !
=" #
serializedObject$ 4
.4 5
FindProperty5 A
(A B
$strB R
)R S
;S T 
_parentNamesProperty  
=! "
serializedObject# 3
.3 4
FindProperty4 @
(@ A
$strA W
)W X
;X Y6
*_parentNewObjectsUnderSceneContextProperty 6
=7 8
serializedObject9 I
.I J
FindPropertyJ V
(V W
$strW {
){ |
;| }
} 	
	protected 
override 
void 
OnGui  %
(% &
)& '
{ 	
base 
. 
OnGui 
( 
) 
; 
EditorGUILayout 
. 
PropertyField )
() *!
_contractNameProperty* ?
,? @
trueA E
)E F
;F G
EditorGUILayout 
. 
PropertyField )
() * 
_parentNamesProperty* >
,> ?
true@ D
)D E
;E F
EditorGUILayout 
. 
PropertyField )
() *6
*_parentNewObjectsUnderSceneContextProperty* T
)T U
;U V
}   	
}!! 
}"" ê
_C:\Users\–ö–ª–∏–º–Ω—é–∫\tring\App\Assets\Plugins\Zenject\Source\Editor\Editors\ContextEditor.cs
	namespace 	
Zenject
 
{ 
[ 
NoReflectionBaking 
] 
public 

class 
ContextEditor 
:  $
UnityInspectorListEditor! 9
{ 
	protected 
override 
string !
[! "
]" #
PropertyNames$ 1
{		 	
get

 
{ 
return 
new 
string !
[! "
]" #
{ 
$str 1
,1 2
$str %
,% &
$str '
,' (
} 
; 
} 
} 	
	protected 
override 
string !
[! "
]" # 
PropertyDisplayNames$ 8
{ 	
get 
{ 
return 
new 
string !
[! "
]" #
{ 
$str 2
,2 3
$str %
,% &
$str '
,' (
} 
; 
} 
}   	
	protected"" 
override"" 
string"" !
[""! "
]""" # 
PropertyDescriptions""$ 8
{## 	
get$$ 
{%% 
return&& 
new&& 
string&& !
[&&! "
]&&" #
{'' 
$str(( c
,((c d
$str)) _
,))_ `
$str** P
,**P Q
}++ 
;++ 
},, 
}-- 	
}.. 
}// 
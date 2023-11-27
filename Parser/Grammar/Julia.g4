grammar Julia;

/* Parser */
start: (function | statement)* EOF;

function: FUNCTION_T IDENTIFIER parameters statement* END_T;
parameters: LPAREN (IDENTIFIER DCOLON type (COMMA IDENTIFIER DCOLON type)*)? RPAREN;
while: WHILE_T expression statement* END_T;

statement: declaration | assignment | call | return | while;
declaration: IDENTIFIER DCOLON type EQ expression;
assignment: IDENTIFIER EQ expression;
call: IDENTIFIER LPAREN (expression (COMMA expression)*)? RPAREN;
return: RETURN_T;

expression: INTCONST | FLTCONST | BOOLCONST | STRCONST | IDENTIFIER;
type: INTEGER_T | FLOAT64_T | BOOL_T | STRING_T;

/* Lexer */

fragment Letter: [a-zA-Z];
fragment Digit: [0-9];
fragment LetterOrDigit: Letter | Digit;

// Tokens
FUNCTION_T: 'function';
RETURN_T: 'return';
BEGIN_T: 'begin';
END_T: 'end';
IF_T: 'if';
ELSE_T: 'else';
WHILE_T: 'while';
TRUE_T: 'true';
FALSE_T: 'false';
INTEGER_T: 'Integer';
FLOAT64_T: 'Float64';
BOOL_T: 'Bool';
STRING_T: 'String';

// Brackets and punctuation
LBRACE: '{';
RBRACE: '}';
LPAREN: '(';
RPAREN: ')';
LBRACK: '[';
RBRACK: ']';
DCOLON: '::';
COMMA: ',';
SEMICOLON: ';';
COLON: ':';
DOT: '.';

// Arithmetic operators
PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
PERCENT: '%';

// Comparison operators
EQEQ: '==';
LT: '<';
GT: '>';
LTE: '<=';
GTE: '>=';
NEQ: '!=';

// Boolean operators
AND: '&&';
OR: '||';
NOT: '!';

// Assignment operators
EQ: '=';

IDENTIFIER: Letter LetterOrDigit*;

// Literals
INTCONST: MINUS? [0-9]+;
FLTCONST: MINUS? [0-9]+ DOT [0-9]+;
STRCONST: '"' .*? '"';
BOOLCONST: TRUE_T | FALSE_T;

// Comments and whitespace
COMMENT: '#' ~[\r\n]* -> skip;
COMMENT_BLOCK: '#=' .*? '=#' -> skip;
WS: [ \t\r\n]+ -> skip;
NL: [\r\n]+ -> skip;
